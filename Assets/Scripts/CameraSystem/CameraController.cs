using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Player.States;
using UnityEngine;
using VContainer;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private PlayerStateController _playerController;

        private Vector3 currentVelocity;

		[Header("Settings")]

		[SerializeField] 
		[Range(0.0f, 1.0f)]
		private float _smoothTime = 0.1f;

		[SerializeField] 
		[Range(0.0f, 100.0f)]
		private float _height = 40f;

		[SerializeField] 
		[Range(45f, 90f)]
		private float _angle = 90f;
		
		[Header("Look Ahead")]
		[SerializeField] 
		[Range(0.0f, 20.0f)]
		private float _horizontalLookAhead = 5f;

		[SerializeField] 
		[Range(0.0f, 20.0f)]
		private float _verticalLookAhead = 5f;

		private void LateUpdate()
		{
			if (_playerController != null)
				HandleMovement();
		}

		private void HandleMovement()
		{
			var zRelativeToAngle =  _height/Mathf.Sin(_angle * Mathf.Deg2Rad) * Mathf.Sin((90 - _angle) * Mathf.Deg2Rad);

			var distanceX = (Input.mousePosition.x - Screen.width * 0.5f) / Screen.width;;
			var distanceZ = (Input.mousePosition.y - Screen.height * 0.5f) / Screen.height;
				
			Vector3 targetPosition = new Vector3(
				_playerController.transform.position.x + distanceX * _horizontalLookAhead,
				_height,
				_playerController.transform.position.z - zRelativeToAngle + distanceZ * _verticalLookAhead
			);
				
			transform.position = Vector3.SmoothDamp(
				transform.position,
				targetPosition,
				ref currentVelocity,
				_smoothTime
			);
		}
    }
}
