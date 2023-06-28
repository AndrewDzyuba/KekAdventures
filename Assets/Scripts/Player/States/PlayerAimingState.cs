using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.States
{
    public class PlayerAimingState : ICharacterState
    {
        private Plane _plane = new Plane(Vector3.up, 0);
        private float _flightTime = 1f;
        private int _lineSegments = 10;

        public void OnEnter(PlayerStateController controller)
        {
            controller.rigidBody.velocity = Vector3.zero;

            controller.lineRenderer.positionCount = _lineSegments;
        }

        public void UpdateState(PlayerStateController controller)
        {
            var mouseWorldPos = GetMouseWorldPos();
            var launchVelocity = GetLaunchVelocity(mouseWorldPos, 1f, controller.throwTransform.position);
            
            if (CheckFireButton(controller, launchVelocity))
                return;

            VisualizeTrajectory(launchVelocity, mouseWorldPos, controller.throwTransform.position, controller.lineRenderer);
        }

        public void FixedUpdateState(PlayerStateController controller)
        {
            HandleRotation(controller.rigidBody, controller.transform);
        }

        public void OnExit(PlayerStateController controller)
        {
            ClearTrajectory(controller);
        }

        private Vector3 GetMouseWorldPos()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            float distance;
            if (!_plane.Raycast(mouseRay, out distance))
                return Vector3.zero;
        
            return mouseRay.GetPoint(distance);
        }

        private Vector3 GetLaunchVelocity(Vector3 mouseWorldPos, float flightTime, Vector3 startingPoint) 
        {
            Vector3 gravityNormal = Physics.gravity.normalized;
            Vector3 dx = Vector3.ProjectOnPlane(mouseWorldPos, gravityNormal) - Vector3.ProjectOnPlane(startingPoint, gravityNormal);
            Vector3 initialVelocityX = dx/flightTime;

            Vector3 dy = Vector3.Project(mouseWorldPos, gravityNormal) - Vector3.Project(startingPoint, gravityNormal);
            Vector3 g = Physics.gravity * (0.5f * (flightTime * flightTime));
            Vector3 initialVelocityY = (dy - g)/flightTime;
            return initialVelocityX + initialVelocityY;
        }

        private void VisualizeTrajectory(Vector3 velocity0, Vector3 finalPos, Vector3 playerPos, LineRenderer lineRenderer)
        {
            for (int i = 0; i < _lineSegments - 1; i++)
            {
                Vector3 pos = CalculatePositionInTime(velocity0, (i / (float)_lineSegments) * _flightTime, playerPos);
                lineRenderer.SetPosition(i, pos);
            }
 
            lineRenderer.SetPosition(_lineSegments - 1, finalPos);
        }

        private Vector3 CalculatePositionInTime(Vector3 velocity0, float time, Vector3 playerPos)
        {
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
                TryThrowGrenade(controller, launchVelocity);
                return true;
            }

            return false;
        }
        
        private void TryThrowGrenade(PlayerStateController controller, Vector3 launchVelocity)
        {
            if (!controller.PlayerAmmo.TrySpendGrenade())
                return;
            
            var prefab = controller.GrenadesData.ThrowPrefab;
            var grenade = Pooling.Spawn(prefab.gameObject, controller.throwTransform.position, controller.throwTransform.rotation).GetComponent<GrenadeThrow>();

            GrenadeType currentType = controller.PlayerAmmo.ChoosedGreande;
            var grenadeData = controller.GrenadesData.GetGrenadeData(currentType);
            grenade.Init(launchVelocity, grenadeData.material, grenadeData.explosionColor);
        }
    }
}
