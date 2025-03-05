using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace SG {
    class TitleScreenManager : MonoBehaviour {

        internal static TitleScreenManager Instance { get; private set; }

        [Header("---------- Menus ----------")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("---------- Buttons ----------")]
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button mainMenuNewGameButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button deleteCharacterPopUpConfirmButton;

        [Header("---------- Pop Ups ----------")]
        [SerializeField] GameObject noCharacterSlotPopUp;
        [SerializeField] Button noCharacterSlotOkayButton;
        [SerializeField] GameObject deleteCharacterSlotPopUp;

        [Header("---------- Character Slots ----------")]
        [SerializeField] CharacterSlots currentSelectedSlot = CharacterSlots.NO_SLOT;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void StartNetworkAsHost() {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame() {
            WorldSaveGameManager.Instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu() {
            titleScreenMainMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu() {
            titleScreenLoadMenu.SetActive(false);
            titleScreenMainMenu.SetActive(true);

            mainMenuLoadGameButton.Select();
        }

        internal void DisplayNoFreeCharacterSlotsPopup() {
            noCharacterSlotPopUp.SetActive(true);
            noCharacterSlotOkayButton.Select();
        }

        public void CloseNoFreeCharacterSlotsPopUp() {
            noCharacterSlotPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }

        internal void SelectedCharacterSlot(CharacterSlots characterSlots) {
            currentSelectedSlot = characterSlots;
        }

        public void SelectNoSlot() {
            currentSelectedSlot = CharacterSlots.NO_SLOT;
        }

        internal void AttemptToDeleteCharacterSlot() {
            if (currentSelectedSlot != CharacterSlots.NO_SLOT) {
                deleteCharacterSlotPopUp.SetActive(true);
                deleteCharacterPopUpConfirmButton.Select();
            }
        }

        public void DeleteCharacterSlot() {
            deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveGameManager.Instance.DeleteGame(currentSelectedSlot);

            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharacterPopUp() {
            deleteCharacterSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }
    }
}
