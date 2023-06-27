using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingState : ICharacterState
{
    public void OnEnter(PlayerStateController controller, Rigidbody rigidBody)
    {
        rigidBody.velocity = Vector3.zero;
    }

    public void UpdateState(PlayerStateController controller, Rigidbody rigidBody, Transform transform)
    {
        CheckFireButton(controller);
    }

    public void FixedUpdateState(PlayerStateController controller, Rigidbody rigidBody, Transform transform)
    {
        HandleRotation(rigidBody, transform);
    }

    public void OnExit(PlayerStateController controller)
    {
        
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
