using Player.States;
using UnityEngine;

namespace System
{
    public interface ICharacterState
    {
        public void OnEnter(PlayerStateController controller);
        public void UpdateState(PlayerStateController controller);
        public void FixedUpdateState(PlayerStateController controller);
        public void OnExit(PlayerStateController controller);
    }
}