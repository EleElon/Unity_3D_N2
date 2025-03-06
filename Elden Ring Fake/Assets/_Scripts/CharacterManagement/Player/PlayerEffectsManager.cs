using UnityEngine;

namespace SG {
    class PlayerEffectsManager : CharacterEffectManager {

        [Header("--------- Debug Delete Later ----------")]
        [SerializeField] InstantCharacterEffect effectToTest;
        [SerializeField] bool processEffect = false;

        void Update() {
            if (processEffect) {
                processEffect = false;

                InstantCharacterEffect effect = Instantiate(effectToTest);
                // effect.SetStaminaDamage(55);

                // TakeStaminaDamageEffect effect = Instantiate(effectToTest) as TakeStaminaDamageEffect;
                // effect.SetStaminaDamage(55);

                ProcessInstantEffect(effect);
            }
        }
    }
}