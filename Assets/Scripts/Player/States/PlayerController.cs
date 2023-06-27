using System;
using System.Collections;
using System.Collections.Generic;
using CameraSystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;

        float _horizontalMove;
        float _frontMove;
        float _moveLimiter = 0.7f;
        float _rotationSpeed = 600f;

        public float _runSpeed = 1.0f;

        void Update()
        {
            _horizontalMove = Input.GetAxisRaw(InputSettings.SIDES_AXES);
            _frontMove = Input.GetAxisRaw(InputSettings.FRONT_AXES);
        }

        void FixedUpdate()
        {
            HandleAxes();
            HandleRotation();

            if (Input.GetKey(InputSettings.FIRE))
                HandleAiming();
            else
                HandleMoving();
        }

        private void HandleAxes()
        {
            if (_horizontalMove != 0 && _frontMove != 0)
            {
                _horizontalMove *= _moveLimiter;
                _frontMove *= _moveLimiter;
            }
        }

        private void HandleMoving()
        {
            _rigidBody.velocity = new Vector3(_horizontalMove * _runSpeed, 0, _frontMove * _runSpeed);
        }

        private void HandleRotation()
        {
            var characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            var direction = Mathf.Atan2(Input.mousePosition.y - characterScreenPos.y, Input.mousePosition.x - characterScreenPos.x);
            _rigidBody.MoveRotation(Quaternion.Euler(0f, -Mathf.Rad2Deg * direction, 0f));
        }

        private void HandleAiming()
        {
            
        }
    }
}

