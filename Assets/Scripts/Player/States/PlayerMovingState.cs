using System;
using UnityEngine;

namespace Player.States
{
    public class PlayerMovingState : ICharacterState
    {
        private float _horizontalMove;
        private float _frontMove;
    
        private float _moveLimiter = 0.7f;
        private float _rotationSpeed = 600f;
        private float _runSpeed = 20.0f;
    
        public void OnEnter(PlayerStateController controller)
        {

        }

        public void UpdateState(PlayerStateController controller)
        {
            CheckFireButton(controller);
        
            _horizontalMove = Input.GetAxisRaw(InputSettings.SIDES_AXES);
            _frontMove = Input.GetAxisRaw(InputSettings.FRONT_AXES);
        }

        public void FixedUpdateState(PlayerStateController controller)
        {
            HandleAxes();
            HandleRotation(controller.rigidBody, controller.transform);
            HandleMoving(controller.rigidBody);
        }

        public void OnExit(PlayerStateController controller)
        {
        
        }
    
        private void HandleAxes()
        {
            if (_horizontalMove != 0 && _frontMove != 0)
            {
                _horizontalMove *= _moveLimiter;
                _frontMove *= _moveLimiter;
            }
        }

        private void HandleMoving(Rigidbody rigidBody)
        {
            rigidBody.velocity = new Vector3(_horizontalMove * _runSpeed, 0, _frontMove * _runSpeed);
        }

        private void HandleRotation(Rigidbody rigidBody, Transform transform)
        {
            var characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            var direction = Mathf.Atan2(Input.mousePosition.y - characterScreenPos.y, Input.mousePosition.x - characterScreenPos.x);
            rigidBody.MoveRotation(Quaternion.Euler(0f, -Mathf.Rad2Deg * direction, 0f));
        }

        private void CheckFireButton(PlayerStateController controller)
        {
            if (Input.GetKeyDown(InputSettings.FIRE) && controller.PlayerAmmo.HaveGrenadeOfSelectedType())
                controller.ChangeState(controller.AimingState);
        }
    }
}
