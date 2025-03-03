using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace SG {
    class WorldSaveGameManager : MonoBehaviour {

        internal static WorldSaveGameManager Instance { get; private set; }

        [SerializeField] PlayerManager _playerManager;

        [Header("---------- Save/Load ----------")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("---------- World Scene Index ----------")]
        int worldSceneIndex = 1;

        [Header("---------- Save Data Writer ----------")]
        SaveFileDataWriter _saveFileDataWriter;

        [Header("---------- Current Character Data ----------")]
        CharacterSlots currentCharacterSlotBeingUsed;
        CharacterSavingData currentCharacterData;
        string saveFileName;

        [Header("---------- Character Slots ----------")]
        CharacterSavingData characterSlot01;
        // CharacterSavingData characterSlot02;
        // CharacterSavingData characterSlot03;
        // CharacterSavingData characterSlot04;
        // CharacterSavingData characterSlot05;
        // CharacterSavingData characterSlot06;
        // CharacterSavingData characterSlot07;
        // CharacterSavingData characterSlot08;
        // CharacterSavingData characterSlot09;
        // CharacterSavingData characterSlot10;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        void Start() {
            DontDestroyOnLoad(gameObject);
        }

        void Update() {
            if (saveGame) {
                saveGame = false;
                SaveGame();
            }

            if (loadGame) {
                loadGame = false;
                LoadGame();
            }
        }

        void DecideCharacterFileNameBasedOnCharacterSlotBeingUsed() {
            switch (currentCharacterSlotBeingUsed) {
                case CharacterSlots.CharacterSlot_01:
                    saveFileName = "characterSlot_01";
                    break;
                case CharacterSlots.CharacterSlot_02:
                    saveFileName = "characterSlot_02";
                    break;
                case CharacterSlots.CharacterSlot_03:
                    saveFileName = "characterSlot_03";
                    break;
                case CharacterSlots.CharacterSlot_04:
                    saveFileName = "characterSlot_04";
                    break;
                case CharacterSlots.CharacterSlot_05:
                    saveFileName = "characterSlot_05";
                    break;
                case CharacterSlots.CharacterSlot_06:
                    saveFileName = "characterSlot_06";
                    break;
                case CharacterSlots.CharacterSlot_07:
                    saveFileName = "characterSlot_07";
                    break;
                case CharacterSlots.CharacterSlot_08:
                    saveFileName = "characterSlot_08";
                    break;
                case CharacterSlots.CharacterSlot_09:
                    saveFileName = "characterSlot_09";
                    break;
                case CharacterSlots.CharacterSlot_10:
                    saveFileName = "characterSlot_10";
                    break;
            }
        }

        internal void CreateNewGame() {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

            currentCharacterData = new CharacterSavingData();
        }

        void LoadGame() {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);
            _saveFileDataWriter.SetSaveFileName(saveFileName);
            currentCharacterData = _saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        void SaveGame() {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();
            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);
            _saveFileDataWriter.SetSaveFileName(saveFileName);

            _playerManager.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

            _saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        internal IEnumerator LoadWorldScene() {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            // yield return null;

            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f) {
                // Debug.Log("Loading: " + loadOperation.progress);
                yield return null;
            }

            yield return new WaitForSeconds(1);
            loadOperation.allowSceneActivation = true;
        }

        internal int GetWorldSceneIndex() {
            return worldSceneIndex;
        }
    }
}
