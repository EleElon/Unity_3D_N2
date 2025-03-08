using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG {
    class PlayerManager : CharacterManager {

        [Header("---------- Debug Menu ----------")]
        [SerializeField] bool respawnCharacter = false;
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
            if (Input.GetKeyDown(KeyCode.K)) {
                _playerNetworkManager.GetnSetCurrentHealth().Value = 0;
            }

            DebugMenu();
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

                _playerNetworkManager.GetnSetVitality().OnValueChanged += _playerNetworkManager.SetNewMaxHealthValue;
                _playerNetworkManager.GetEndurace().OnValueChanged += _playerNetworkManager.SetNewMaxStaminaValue;

                _playerNetworkManager.GetnSetCurrentHealth().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewHealthValue;
                _playerNetworkManager.GetnSetCurrentEaseHealth().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewEaseHealthValue;
                // _playerNetworkManager.GetnSetCurrentHealth().OnValueChanged += _playerStatManager.ResetRegenerationTimer;

                _playerNetworkManager.GetCurrentStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewStaminaValue;
                _playerNetworkManager.GetCurrentEaseStamina().OnValueChanged += PlayerUIManager.Instance.GetPlayerUIHudManager().SetNewEaseStaminaValue;
                _playerNetworkManager.GetCurrentStamina().OnValueChanged += _playerStatManager.ResetRegenerationTimer;
            }

            _playerNetworkManager.GetnSetCurrentHealth().OnValueChanged += _playerNetworkManager.CheckHP;
        }

        internal override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false) {
            if (IsOwner) {
                PlayerUIManager.Instance.GetPlayerUIPopUpManager().SendYouDiedPopUp();
            }

            return base.ProcessDeathEvent(manuallySelectDeathAnimation);
        }

        internal void SaveGameDataToCurrentCharacterData(ref CharacterSavingData currentCharacterData) {
            currentCharacterData.SetSceneIndex(SceneManager.GetActiveScene().buildIndex);
            currentCharacterData.SetCharacterName(_playerNetworkManager.GetnSetCharacterName().Value.ToString());

            currentCharacterData.SetXPosition(transform);
            currentCharacterData.SetYPosition(transform);
            currentCharacterData.SetZPosition(transform);

            currentCharacterData.SetVitality(_playerNetworkManager.GetnSetVitality().Value);
            currentCharacterData.SetCurrentHealth(_playerNetworkManager.GetnSetCurrentHealth().Value);

            currentCharacterData.SetEndurance(_playerNetworkManager.GetEndurace().Value);
            currentCharacterData.SetCurrentStamina(_playerNetworkManager.GetCurrentStamina().Value);
        }

        internal void LoadGameDataFromCharacterData(ref CharacterSavingData currentCharacterData) {
            _playerNetworkManager.GetnSetCharacterName().Value = currentCharacterData.GetCharacterName();

            Vector3 myPosition = new Vector3(currentCharacterData.GetXPosition(), currentCharacterData.GetYPosition(), currentCharacterData.GetZPosition());

            transform.position = myPosition;

            //* Stat
            _playerNetworkManager.GetnSetVitality().Value = currentCharacterData.GetVitality();
            _playerNetworkManager.SetEndurance(currentCharacterData.GetEndurance());

            _playerNetworkManager.GetnSetMaxHealth().Value = _playerStatManager.CalculateHealthBasedOnVitalityLevel(_playerNetworkManager.GetnSetVitality().Value);
            _playerNetworkManager.SetMaxStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace().Value));

            _playerNetworkManager.GetnSetCurrentHealth().Value = currentCharacterData.GetCurrentHealth();
            _playerNetworkManager.SetCurrentStamina(currentCharacterData.GetCurrentStamina());

            _playerNetworkManager.GetnSetCurrentHealth().Value = _playerNetworkManager.GetnSetMaxHealth().Value;
            _playerNetworkManager.SetCurrentStamina(_playerNetworkManager.GetMaxStamina());

            _playerNetworkManager.GetnSetCurrentEaseHealth().Value = _playerStatManager.CalculateHealthBasedOnVitalityLevel(_playerNetworkManager.GetnSetVitality().Value);
            _playerNetworkManager.SetCurrentEaseStamina(_playerStatManager.CalculateStaminaBasedOnEnduranceLevel(_playerNetworkManager.GetEndurace().Value));

            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxHealthValue(_playerNetworkManager.GetnSetMaxHealth().Value);
            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxEaseHealthValue(_playerNetworkManager.GetnSetCurrentHealth().Value);

            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxStaminaValue(_playerNetworkManager.GetMaxStamina());
            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxEaseStaminaValue(_playerNetworkManager.GetCurrentStamina().Value);
        }

        protected override void ReviveCharacter() {
            base.ReviveCharacter();

            if (IsOwner) {
                _playerNetworkManager.GetnSetCurrentHealth().Value = _playerNetworkManager.GetnSetMaxHealth().Value;

                _playerAnimatorManager.PlayTargetActionAnimation("Empty", false);
            }
        }

        void DebugMenu() {
            if (respawnCharacter) {
                respawnCharacter = false;
                ReviveCharacter();
            }
        }

        internal PlayerAnimatorManager GetPlayerAnimatorManager() { return _playerAnimatorManager; }

        internal PlayerLocomotionManager GetPlayerLocomotionManager() { return _playerLocomotionManager; }

        internal PlayerNetworkManager GetPlayerNetworkManager() { return _playerNetworkManager; }

        internal PlayerStatManager GetPlayerStatManager() { return _playerStatManager; }
    }
}
