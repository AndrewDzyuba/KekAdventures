using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.States
{
    public class PlayerAimingState : ICharacterState
    {
        Plane plane = new Plane(Vector3.up, 0);
        private float _trajectoryVertDist = 0.25f;
        private float _maxCurveLength = 5;
        
        private float _flightTime = 1f;
        public int _lineSegments = 10;

        public void OnEnter(PlayerStateController controller)
        {
            controller.rigidBody.velocity = Vector3.zero;
        }

        public void UpdateState(PlayerStateController controller)
        {
            var velocity = GetLaunchVelocity(1f, controller.throwTransform.position);
            
            if (CheckFireButton(controller, velocity))
                return;

            DrawTrajectory(controller);
        }

        public void FixedUpdateState(PlayerStateController controller)
        {
            HandleRotation(controller.rigidBody, controller.transform);
        }

        public void OnExit(PlayerStateController controller)
        {
            ClearTrajectory(controller);
        }

        private Vector3 GetLaunchVelocity(float flightTime, Vector3 startingPoint) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            float distance;
            Vector3 worldPosition = Vector3.zero;
            if (!plane.Raycast(mouseRay, out distance))
                return Vector3.zero;
        
            worldPosition = mouseRay.GetPoint(distance);
            
            Vector3 gravityNormal = Physics.gravity.normalized;
            Vector3 dx = Vector3.ProjectOnPlane(worldPosition, gravityNormal) - Vector3.ProjectOnPlane(startingPoint, gravityNormal);
            Vector3 initialVelocityX = dx/flightTime;

            Vector3 dy = Vector3.Project(worldPosition, gravityNormal) - Vector3.Project(startingPoint, gravityNormal);
            Vector3 g = 0.5f * Physics.gravity * (flightTime * flightTime);
            Vector3 initialVelocityY = (dy - g)/flightTime;
            return initialVelocityX + initialVelocityY;
        }

        private void DrawTrajectory(PlayerStateController controller)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            float distance;
            Vector3 worldPosition = Vector3.zero;
            if (!plane.Raycast(mouseRay, out distance))
                return;
        
            worldPosition = mouseRay.GetPoint(distance);
            /*Vector3 velocity0 = CalculateVelocity(worldPosition, controller.transform.position, _flightTime);
            Visualize(velocity0, worldPosition, controller.transform.position, controller.lineRenderer);
            
            
            var currentPosition = controller.transform.position;
            var currentVelocity = controller.transform.rotation.eulerAngles;
            
            var curvePoints = new List<Vector3>();
            curvePoints.Add(currentPosition);
            curvePoints.Add(worldPosition);

            controller.lineRenderer.positionCount = curvePoints.Count;
            controller.lineRenderer.SetPositions(curvePoints.ToArray());*/
        }
        
        void Visualize(Vector3 velocity0, Vector3 finalPos, Vector3 playerPos, LineRenderer lineRenderer)
        {
            for (int i = 0; i < _lineSegments; i++)
            {
                Vector3 pos = CalculatePositionInTime(velocity0, (i / (float)_lineSegments) * _flightTime, playerPos);
                lineRenderer.SetPosition(i, pos);
            }
 
            lineRenderer.SetPosition(_lineSegments, finalPos);
        }
        
        private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0f;
 
            float pathY = distance.y;
            float pathXZ = distanceXZ.magnitude;
 
            float VelocityXZ = pathXZ / time;
            float VelocityY = (pathY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
 
            Vector3 result = distanceXZ.normalized;
            result *= VelocityXZ;
            result.y = VelocityY;
 
            return result;
        }
        
        private Vector3 CalculatePositionInTime(Vector3 velocity0, float time, Vector3 playerPos)
        {
            Vector3 velocityXZ = velocity0;
            velocityXZ.y = 0f;
 
            Vector3 result = playerPos + velocity0 * time;
            float pathY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity0.y * time) + playerPos.y;
 
            result.y = pathY;
 
            return result;
        }
        
        private void ClearTrajectory(PlayerStateController controller)
        {
            controller.lineRenderer.positionCount = 0;
        }
    
        private void HandleRotation(Rigidbody rigidBody, Transform transform)
        {
            var characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            var direction = Mathf.Atan2(Input.mousePosition.y - characterScreenPos.y, Input.mousePosition.x - characterScreenPos.x);
            rigidBody.MoveRotation(Quaternion.Euler(0f, -Mathf.Rad2Deg * direction, 0f));
        }
    
        private bool CheckFireButton(PlayerStateController controller, Vector3 launchVelocity)
        {
            if (Input.GetKeyUp(InputSettings.FIRE))
            {
                controller.ChangeState(controller.MovingState);
                ThrowGrenade(controller, launchVelocity);
                return true;
            }

            return false;
        }
        
        private void ThrowGrenade(PlayerStateController controller, Vector3 launchVelocity)
        {
            var prefab = controller.GrenadesData.ThrowPrefab;
            var grenade = Pooling.Spawn(prefab.gameObject, controller.throwTransform.position, controller.throwTransform.rotation).GetComponent<GrenadeThrow>();

            GrenadeType currentType = controller.PlayerAmmo.ChoosedGreande;
            var grenadeData = controller.GrenadesData.GetGrenadeData(currentType);
            grenade.Init(launchVelocity, grenadeData.material, grenadeData.explosionColor);
        }
    }
}
