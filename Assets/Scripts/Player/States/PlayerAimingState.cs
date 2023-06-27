using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.States
{
    public class PlayerAimingState : ICharacterState
    {
        private Vector3 _startPosition;
        private Vector3 _startVelocity;
        private float _trajectoryVertDist = 0.25f;
        private float _maxCurveLength = 5;
        
        public void OnEnter(PlayerStateController controller)
        {
            controller.rigidBody.velocity = Vector3.zero;
        }

        public void UpdateState(PlayerStateController controller)
        {
            SetBallisticValues(controller.transform.position, controller.transform.rotation.eulerAngles);
            DrawTrajectory(controller);
            CheckFireButton(controller);
        }

        public void FixedUpdateState(PlayerStateController controller)
        {
            HandleRotation(controller.rigidBody, controller.transform);
        }

        public void OnExit(PlayerStateController controller)
        {
            ClearTrajectory(controller);
        }
        
        private void SetBallisticValues(Vector3 startPosition, Vector3 startVelocity)
        {
            _startPosition = startPosition;
            _startVelocity = startVelocity;
        }
        
        private void DrawTrajectory(PlayerStateController controller)
        {
            var curvePoints = new List<Vector3>();
            curvePoints.Add(_startPosition);
            
            var currentPosition = _startPosition;
            var currentVelocity = _startPosition;
            
            RaycastHit hit;
            Ray ray = new Ray(currentPosition, currentVelocity.normalized);
           
            while (!Physics.Raycast(ray, out hit, _trajectoryVertDist) && Vector3.Distance(_startPosition, currentPosition) < _maxCurveLength)
            {
                var trajectoryTime = _trajectoryVertDist / currentVelocity.magnitude;
                
                currentVelocity = currentVelocity + trajectoryTime * Physics.gravity;
                currentPosition = currentPosition + trajectoryTime * currentVelocity;
                curvePoints.Add(currentPosition);
                
                ray = new Ray(currentPosition, currentVelocity.normalized);
            }
            
            if (hit.transform)
            {
                curvePoints.Add(hit.point);
            }
            
            controller.lineRenderer.positionCount = curvePoints.Count;
            controller.lineRenderer.SetPositions(curvePoints.ToArray());
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
    
        private void CheckFireButton(PlayerStateController controller)
        {
            if (Input.GetKeyUp(InputSettings.FIRE))
                controller.ChangeState(controller.MovingState);
        }
    }
}
