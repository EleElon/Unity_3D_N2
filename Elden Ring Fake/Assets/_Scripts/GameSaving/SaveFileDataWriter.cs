using UnityEngine;
using System;
using System.IO;

namespace SG {
    class SaveFileDataWriter {

        string saveDataDirectoryPath;
        string saveFileName;

        internal bool CheckToSeeIfFileExists() {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName))) {
                return true;
            }
            else {
                return false;
            }
        }

        internal void DeleteSaveFile() {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        internal void CreateNewCharacterSaveFile(CharacterSavingData characterSavingData) {
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Create save file, at save path: " + savePath);

                string dataToStore = JsonUtility.ToJson(characterSavingData, true);

                using (FileStream stream = new FileStream(savePath, FileMode.Create)) {
                    using (StreamWriter fileWriter = new StreamWriter(stream)) {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (System.Exception ex) {
                Debug.LogError("Error whilst trying to save character data, game not saved" + savePath + "\n" + ex);
            }
        }

        internal CharacterSavingData LoadSaveFile() {
            CharacterSavingData characterData = null;
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath)) {
                try {
                    string dataToLoad;

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open)) {
                        using (StreamReader reader = new StreamReader(stream)) {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    characterData = JsonUtility.FromJson<CharacterSavingData>(dataToLoad);
                }
                catch (System.Exception ex) {
                    Debug.Log("File is blank" + loadPath + "\n" + ex);
                }
            }
            return characterData;
        }

        internal void SetSaveDataDirectoryPath(string path) {
            saveDataDirectoryPath = path;
        }

        internal void SetSaveFileName(string fileName) {
            saveFileName = fileName;
        }
    }
}
