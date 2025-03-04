using UnityEngine;
using TMPro;

namespace SG {
    class UI_Character_Save_Slot : MonoBehaviour {

        SaveFileDataWriter _saveFileDataWriter;

        [Header("---------- Game Slots ----------")]
        [SerializeField] CharacterSlots _characterSlots;

        [Header("---------- Character Info ----------")]
        [SerializeField] TextMeshProUGUI characterName;
        [SerializeField] TextMeshProUGUI timePlayed;

        void OnEnable() {
            LoadSaveSlots();
        }

        void LoadSaveSlots() {
            _saveFileDataWriter = new SaveFileDataWriter();
            _saveFileDataWriter.SetSaveDataDirectoryPath(Application.persistentDataPath);

            switch (_characterSlots) {
                case CharacterSlots.CharacterSlot_00:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot00().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_01:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot01().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_02:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot02().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_03:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot03().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_04:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot04().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_05:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot05().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_06:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot06().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_07:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot07().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_08:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot08().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
                case CharacterSlots.CharacterSlot_09:
                    _saveFileDataWriter.SetSaveFileName(WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(_characterSlots));

                    if (_saveFileDataWriter.CheckToSeeIfFileExists()) {
                        characterName.text = WorldSaveGameManager.Instance.GetCharacterSlot09().GetCharacterName();
                    }
                    else {
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }

        public void LoadGameFromCharacterSlot() {
            WorldSaveGameManager.Instance.SetCurrentCharacterSlotBeingUsed(_characterSlots);
            WorldSaveGameManager.Instance.LoadGame();
        }
    }
}
