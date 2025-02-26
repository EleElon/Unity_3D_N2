using UnityEngine;

namespace SG {
    class PlayerManager : CharacterManager {

        PlayerLocomotionManager _playerLocomotionManager;
        PlayerAnimatorManager _playerAnimatorManager;
        PlayerNetworkManager _playerNetworkManager;

        protected override void Awake() {
            base.Awake();

            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerNetworkManager = GetComponent<PlayerNetworkManager>();
        }

        protected override void Update() {
            base.Update();

            if (!IsOwner)
                return;

            _playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate() {
            if (!IsOwner)
                return;

            base.LateUpdate();

            PlayerCameraManager.Instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn() {
            base.OnNetworkSpawn();

            if (IsOwner) {
                PlayerCameraManager.Instance.SetPlayerManager(this);
                PlayerInputManager.Instance.SetPlayerManager(this);
            }
        }

        internal PlayerAnimatorManager GetPlayerAnimatorManager() {
            return _playerAnimatorManager;
        }

        internal PlayerLocomotionManager GetPlayerLocomotionManager() {
            return _playerLocomotionManager;
        }

        internal PlayerNetworkManager GetPlayerNetworkManager() {
            return _playerNetworkManager;
        }
    }
}
