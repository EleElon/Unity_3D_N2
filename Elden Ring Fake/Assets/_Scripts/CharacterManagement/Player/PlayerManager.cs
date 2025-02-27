using UnityEngine;

namespace SG {
    class PlayerManager : CharacterManager {

        PlayerLocomotionManager _playerLocomotionManager;
        PlayerAnimatorManager _playerAnimatorManager;
        PlayerNetworkManager _playerNetworkManager;
        PlayerStatManager _playerStatManager;

        protected override void Awake() {
            base.Awake();

            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerNetworkManager = GetComponent<PlayerNetworkManager>();
            _playerStatManager = GetComponent<PlayerStatManager>();
        }

        protected override void Update() {
            base.Update();

            if (!IsOwner)
                return;

            _playerLocomotionManager.HandleAllMovement();

            _playerStatManager.RegenerateStamina();
            _playerStatManager.ChangeEaseStamina();
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
                _playerNetworkManager.GetCurrentStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewStaminaValue;
                //FIXME: fixed ease stamina bar
                _playerNetworkManager.GetCurrentEaseStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewEaseStaminaValue;
                _playerNetworkManager.GetCurrentStamina().OnValueChanged += _playerStatManager.ResetRegenerationTimer;

                _playerNetworkManager.SetMaxStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace()));

                _playerNetworkManager.SetCurrentStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace()));

                PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxStaminaValue(_playerNetworkManager.GetMaxStamina());
                //FIXME: fixed ease stamina bar
                PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxEaseStaminaValue(_playerNetworkManager.GetMaxStamina());
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
