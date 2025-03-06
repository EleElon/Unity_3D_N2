using System.Collections.Generic;
using UnityEngine;

namespace SG {
    class WorldCharacterEffectManager : MonoBehaviour {

        internal static WorldCharacterEffectManager Instance { get; private set; }

        [SerializeField] List<InstantCharacterEffect> instantEffect;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            GenerateEffectID();
        }

        void GenerateEffectID() {
            for (int i = 0; i < instantEffect.Count; i++) {
                instantEffect[i].SetInstanceEffectID(i);
            }
        }
    }
}