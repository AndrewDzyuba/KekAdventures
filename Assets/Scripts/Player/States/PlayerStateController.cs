using System;
using UnityEngine;
using VContainer;

namespace Player.States
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] public Rigidbody rigidBody;
        [SerializeField] public LineRenderer lineRenderer;
        [SerializeField] public Transform throwTransform;

        public PlayerMovingState MovingState { get; private set; } = new PlayerMovingState();
        public PlayerAimingState AimingState { get; private set; } = new PlayerAimingState();
        
        [Inject] private GrenadesData _grenadesData;
        public GrenadesData GrenadesData => _grenadesData;
        
        [Inject] private PlayerAmmo _playerAmmo;
        public PlayerAmmo PlayerAmmo => _playerAmmo;
        
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
