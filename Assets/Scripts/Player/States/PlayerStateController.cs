using System;
using UnityEngine;

namespace Player.States
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] public Rigidbody rigidBody;
        [SerializeField] public LineRenderer lineRenderer;

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
                _currentState.UpdateState(this);
            }
        }

        private void FixedUpdate()
        {
            if (_currentState != null)
            {
                _currentState.FixedUpdateState(this);
            }
        }

        public void ChangeState(ICharacterState newState)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(this);
            }
        
            _currentState = newState;
            _currentState.OnEnter(this);
        }
    }
}
