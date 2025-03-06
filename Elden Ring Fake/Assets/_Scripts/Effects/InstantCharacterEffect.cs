using UnityEngine;

namespace SG {
    class InstantCharacterEffect : ScriptableObject {

        [Header("---------- Effect ID ----------")]
        int instantEffectID;

        internal virtual void ProcessEffect(CharacterManager character) {

        }

        internal void SetInstanceEffectID(int id) {
            instantEffectID = id;
        }
    }
}