using UnityEngine;

namespace SG {
    class PlayerManager : CharacterManager {

        PlayerLocomotionManager _playerLocomotionManager;

        protected override void Awake() {
            base.Awake();

            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update() {
            base.Update();

            if (!IsOwner)
                return;

            _playerLocomotionManager.HandleAllMovement();
        }
    }
}
