using UnityEngine;
using Unity.Netcode;

namespace SG {
    class TitleScreenManager : MonoBehaviour {

        public void StartNetworkAsHost() {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame() {
            WorldSaveGameManager.Instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.Instance.LoadWorldScene());
        }
    }
}
