using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;

    public PlayerMovingState MovingState { get; private set; } = new PlayerMovingState();
    public PlayerAimingState AimingState { get; private set; } = new PlayerAimingState();
    
    private ICharacterState _currentState;

    private void Start()
    {
        ChangeState(MovingState);
    }
    
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this, _rigidBody, transform);
        }
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
        {
            _currentState.FixedUpdateState(this, _rigidBody, transform);
        }
    }

    public void ChangeState(ICharacterState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }
        
        _currentState = newState;
        _currentState.OnEnter(this, _rigidBody);
    }
}
