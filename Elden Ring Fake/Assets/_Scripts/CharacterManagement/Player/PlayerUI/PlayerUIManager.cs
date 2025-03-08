using UnityEngine;
using Unity.Netcode;

namespace SG {
    class PlayerUIManager : MonoBehaviour {

        internal static PlayerUIManager Instance { get; private set; }

        [Header("---------- Network Join ----------")]
        [SerializeField] bool startGameAsClient;

        PlayerUIHudManager _playerUIHudManager;
        PlayerUIPopUpManager _playerUIPopUpManager;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            _playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
            _playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
        }

        void Start() {
            DontDestroyOnLoad(gameObject);
        }

        void Update() {
            if (startGameAsClient) {
                startGameAsClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }

        internal PlayerUIHudManager GetPlayerUIHudManager() { return _playerUIHudManager; }

        internal PlayerUIPopUpManager GetPlayerUIPopUpManager() { return _playerUIPopUpManager; }
    }
}
