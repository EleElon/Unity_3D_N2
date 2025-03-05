using UnityEngine;
using UnityEngine.SceneManagement;

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
            _playerStatManager.SetEaseTimer();
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
                WorldSaveGameManager.Instance.SetPlayerManager(this);

                _playerNetworkManager.GetCurrentStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewStaminaValue;
                _playerNetworkManager.GetCurrentEaseStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewEaseStaminaValue;
                _playerNetworkManager.GetCurrentStamina().OnValueChanged += _playerStatManager.ResetRegenerationTimer;

                _playerNetworkManager.SetMaxStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace()));

                _playerNetworkManager.SetCurrentStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace()));

                _playerNetworkManager.SetCurrentEaseStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace()));

                PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxStaminaValue(_playerNetworkManager.GetMaxStamina());
                PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxEaseStaminaValue(_playerNetworkManager.GetCurrentStamina().Value);
            }
        }

        internal void SaveGameDataToCurrentCharacterData(ref CharacterSavingData currentCharacterData) {
            currentCharacterData.SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
            currentCharacterData.SetCharacterName(_playerNetworkManager.GetnSetCharacterName().Value.ToString());

            currentCharacterData.SetXPosition(transform);
            currentCharacterData.SetYPosition(transform);
            currentCharacterData.SetZPosition(transform);
        }

        internal void LoadGameDataFromCharacterData(ref CharacterSavingData currentCharacterData) {
            _playerNetworkManager.GetnSetCharacterName().Value = currentCharacterData.GetCharacterName();

            Vector3 myPosition = new Vector3(currentCharacterData.GetXPosition(), currentCharacterData.GetYPosition(), currentCharacterData.GetZPosition());

            transform.position = myPosition;
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
