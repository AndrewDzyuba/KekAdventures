using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using VContainer;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;


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
			if(_playerController != null) 
			{
				Vector3 dir = _playerController.transform.position - transform.position;
				Debug.DrawRay(transform.position, dir, Color.red);

				// Use law of sines to calculate Z distance based in camera angle.
				var zRelativeToAngle =  _height/Mathf.Sin(_angle * Mathf.Deg2Rad) * Mathf.Sin((90 - _angle) * Mathf.Deg2Rad);

				var dx = (Input.mousePosition.x - Screen.width * 0.5f) / Screen.width;;
				var dz = (Input.mousePosition.y - Screen.height * 0.5f) / Screen.height;

				// Set Target Position
				Vector3 targetPosition = new Vector3(
					_playerController.transform.position.x + dx * _horizontalLookAhead,
					_height,
					_playerController.transform.position.z - zRelativeToAngle + dz * _verticalLookAhead
				);

				// Set Current Position
				transform.position = Vector3.SmoothDamp(
					transform.position,
					targetPosition,
					ref currentVelocity,
					_smoothTime
				);
			}
			else {
				Debug.LogWarning("Target or Top Collider not found!");
			}
		}
    }
}
