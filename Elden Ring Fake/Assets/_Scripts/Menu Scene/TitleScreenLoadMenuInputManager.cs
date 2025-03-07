using UnityEngine;

namespace SG {
    class TitleScreenLoadMenuInputManager : MonoBehaviour {

        PlayerControls _playerControls;

        UI_Character_Save_Slot _UI_Character_Save_Slot;

        [Header("---------- Title Screen Inputs ----------")]
        bool deleteCharacterSlot = false;
        bool chooseCharacterSlot = false;

        void OnEnable() {
            _UI_Character_Save_Slot = GetComponentInChildren<UI_Character_Save_Slot>();
            if (_playerControls == null) {
                _playerControls = new PlayerControls();

                _playerControls.UI.X.performed += i => deleteCharacterSlot = true;
                _playerControls.UI.Y.performed += i => chooseCharacterSlot = true;
            }

            _playerControls.Enable();
        }

        void OnDisable() {
            _playerControls.Disable();
        }

        void Update() {
            if (deleteCharacterSlot) {
                deleteCharacterSlot = false;
                TitleScreenManager.Instance.AttemptToDeleteCharacterSlot();
            }

            if (chooseCharacterSlot) {
                chooseCharacterSlot = false;
                _UI_Character_Save_Slot.LoadGameFromCharacterSlot();
            }
        }
    }
}
