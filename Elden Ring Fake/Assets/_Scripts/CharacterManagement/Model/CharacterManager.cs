using UnityEngine;

namespace SG {
    class CharacterManager : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
