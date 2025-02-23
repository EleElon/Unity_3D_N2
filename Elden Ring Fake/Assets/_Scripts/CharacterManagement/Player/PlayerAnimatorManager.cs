using UnityEngine;

namespace SG {
    class PlayerAnimatorManager : CharacterAnimatorManager {

        PlayerManager _playerManager;

        protected override void Awake() {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }
        void OnAnimatorMove() {
            if (_playerManager.GetApplyRootMotion()) {
                Vector3 velocity = _playerManager.GetAnimator().deltaPosition;
                _playerManager.GetCharacterController().Move(velocity);
                _playerManager.transform.rotation *= _playerManager.GetAnimator().deltaRotation;
            }
        }
    }
}
