using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace SG {
    class WorldSaveGameManager : MonoBehaviour {

        internal static WorldSaveGameManager Instance { get; private set; }

        PlayerManager _playerManager;

        [Header("---------- Save/Load ----------")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("---------- World Scene Index ----------")]
        int worldSceneIndex = 1;

        [Header("---------- Save Data Writer ----------")]
        SaveFileDataWriter _saveFileDataWriter;

        [Header("---------- Current Character Data ----------")]
        CharacterSlots currentCharacterSlotBeingUsed;
        [SerializeField] CharacterSavingData currentCharacterData;
        string saveFileName;

        [Header("---------- Character Slots ----------")]
        CharacterSavingData characterSlot00;
        CharacterSavingData characterSlot01;
        CharacterSavingData characterSlot02;
        CharacterSavingData characterSlot03;
        CharacterSavingData characterSlot04;
        CharacterSavingData characterSlot05;
        CharacterSavingData characterSlot06;
        CharacterSavingData characterSlot07;
        CharacterSavingData characterSlot08;
        CharacterSavingData characterSlot09;

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
            LoadAllCharacterProfile();
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

        internal string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots characterSlots) {
            string fileName = "";

            switch (characterSlots) {
                case CharacterSlots.CharacterSlot_00:
                    fileName = "characterSlot_00";
                    break;
                case CharacterSlots.CharacterSlot_01:
                    fileName = "characterSlot_01";
                    break;
                case CharacterSlots.CharacterSlot_02:
                    fileName = "characterSlot_02";
                    break;
                case CharacterSlots.CharacterSlot_03:
                    fileName = "characterSlot_03";
                    break;
                case CharacterSlots.CharacterSlot_04:
                    fileName = "characterSlot_04";
                    break;
                case CharacterSlots.CharacterSlot_05:
                    fileName = "characterSlot_05";
                    break;
                case CharacterSlots.CharacterSlot_06:
                    fileName = "characterSlot_06";
                    break;
                case CharacterSlots.CharacterSlot_07:
                    fileName = "characterSlot_07";
                    break;
                case CharacterSlots.CharacterSlot_08:
                    fileName = "characterSlot_08";
                    break;
                case CharacterSlots.CharacterSlot_09:
                    fileName = "characterSlot_09";
                    break;
            }
            return fileName;
        }

        internal void AttemptToCreateNewGame() {
            _saveFileDataWriter = new SaveFileDataWriter();

            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_00));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_00;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_01));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_01;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_02));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_02;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_03));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_03;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_04));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_04;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_05));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_05;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_06));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_06;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_07));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_07;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_08));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_08;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_09));

            if (!_saveFileDataWriter.CheckToSeeIfFileExists()) {
                currentCharacterSlotBeingUsed = CharacterSlots.CharacterSlot_09;

                currentCharacterData = new CharacterSavingData();

                StartCoroutine(LoadWorldScene());

                return;
            }

            TitleScreenManager.Instance.DisplayNoFreeCharacterSlotsPopup();
        }

        internal void LoadGame() {
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);
            _saveFileDataWriter.SetSaveFileName(saveFileName);
            currentCharacterData = _saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        void SaveGame() {
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);
            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);
            _saveFileDataWriter.SetSaveFileName(saveFileName);

            _playerManager.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

            _saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        internal void DeleteGame(CharacterSlots characterSlots) {
            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots));

            _saveFileDataWriter.DeleteSaveFile();
        }

        internal IEnumerator LoadWorldScene() {
            //* if just want 1 world scene, uset this
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            //* if want to use different scenes for levels in project, use this
            // AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentCharacterData.GetSceneIndex());

            _playerManager.LoadGameDataFromCharacterData(ref currentCharacterData);

            // yield return null;

            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f) {
                // Debug.Log("Loading: " + loadOperation.progress);
                yield return null;
            }

            yield return new WaitForSeconds(1);
            loadOperation.allowSceneActivation = true;
        }

        void LoadAllCharacterProfile() {
            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_00));
            characterSlot00 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_01));
            characterSlot01 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_02));
            characterSlot02 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_03));
            characterSlot03 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_04));
            characterSlot04 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_05));
            characterSlot05 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_06));
            characterSlot06 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_07));
            characterSlot07 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_08));
            characterSlot08 = _saveFileDataWriter.LoadSaveFile();

            _saveFileDataWriter.SetSaveFileName(DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_09));
            characterSlot09 = _saveFileDataWriter.LoadSaveFile();
        }

        internal int GetWorldSceneIndex() {
            return worldSceneIndex;
        }

        internal void SetCurrentCharacterSlotBeingUsed(CharacterSlots characterSlots) {
            currentCharacterSlotBeingUsed = characterSlots;
        }

        internal void SetPlayerManager(PlayerManager playerManager) {
            _playerManager = playerManager;
        }

        internal CharacterSavingData GetCharacterSlot00() {
            return characterSlot00;
        }

        internal CharacterSavingData GetCharacterSlot01() {
            return characterSlot01;
        }

        internal CharacterSavingData GetCharacterSlot02() {
            return characterSlot02;
        }

        internal CharacterSavingData GetCharacterSlot03() {
            return characterSlot03;
        }

        internal CharacterSavingData GetCharacterSlot04() {
            return characterSlot04;
        }

        internal CharacterSavingData GetCharacterSlot05() {
            return characterSlot05;
        }

        internal CharacterSavingData GetCharacterSlot06() {
            return characterSlot06;
        }

        internal CharacterSavingData GetCharacterSlot07() {
            return characterSlot07;
        }

        internal CharacterSavingData GetCharacterSlot08() {
            return characterSlot08;
        }

        internal CharacterSavingData GetCharacterSlot09() {
            return characterSlot09;
        }
    }
}
