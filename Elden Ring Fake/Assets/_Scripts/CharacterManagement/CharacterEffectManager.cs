using UnityEngine;

namespace SG {
    class CharacterEffectManager : MonoBehaviour {

        CharacterManager _characterManager;

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(_characterManager);
        }
    }
}