using UnityEngine;
using Unity.Netcode;

namespace SG {
    class TitleScreenManager : MonoBehaviour {
        public void StartNetworkAsHost() {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame() {
            StartCoroutine(WorldSaveGameManager.Instance.LoadNewGame());
        }
    }
}
