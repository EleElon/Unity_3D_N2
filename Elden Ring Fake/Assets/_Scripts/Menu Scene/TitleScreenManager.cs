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

        [Header("---------- Pop Ups ----------")]
        [SerializeField] GameObject noCharacterSlotPopUp;
        [SerializeField] Button noCharacterSlotOkayButton;

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
    }
}
