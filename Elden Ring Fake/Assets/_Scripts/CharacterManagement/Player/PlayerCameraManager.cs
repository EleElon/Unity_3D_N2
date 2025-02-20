using UnityEngine;

namespace SG {
    class PlayerCameraManager : MonoBehaviour {

        internal static PlayerCameraManager Instance { get; private set; }

        [SerializeField] Camera cameraObject;

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

        internal Camera GetCamera() {
            return cameraObject;
        }
    }
}
