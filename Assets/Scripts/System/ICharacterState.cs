using UnityEngine;

namespace System
{
    public interface ICharacterState
    {
        public void OnEnter(PlayerStateController controller, Rigidbody rigidBody);
        public void UpdateState(PlayerStateController controller, Rigidbody rigidBody, Transform transform);
        public void FixedUpdateState(PlayerStateController controller, Rigidbody rigidBody, Transform transform);
        public void OnExit(PlayerStateController controller);
    }
}