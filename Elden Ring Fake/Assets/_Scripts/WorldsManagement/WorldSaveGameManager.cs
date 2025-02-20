using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG {
    class WorldSaveGameManager : MonoBehaviour {
        internal static WorldSaveGameManager Instance { get; private set; }

        int worldSceneIndex = 1;

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

        internal IEnumerator LoadNewGame() {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }

        internal int GetWorldSceneIndex() {
            return worldSceneIndex;
        }
    }
}
