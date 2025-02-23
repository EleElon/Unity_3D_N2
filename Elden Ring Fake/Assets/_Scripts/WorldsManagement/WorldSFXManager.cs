using UnityEngine;

namespace SG {
    class WorldSFXManager : MonoBehaviour {

        internal static WorldSFXManager Instance { get; private set; }

        [Header("---------- Action Sounds ----------")]
        [SerializeField] AudioClip rollSFX;

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

        internal AudioClip GetRollSFX() {
            return rollSFX;
        }
    }
}
