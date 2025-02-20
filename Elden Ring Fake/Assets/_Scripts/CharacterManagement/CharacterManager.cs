using UnityEngine;

namespace SG {
    class CharacterManager : MonoBehaviour {
        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Update() {
            
        }
    }
}
