using UnityEngine;

namespace SG {
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damagek")]
    class TakeStaminaDamageEffect : InstantCharacterEffect {

        float staminaDamage = 25;

        internal override void ProcessEffect(CharacterManager character) {
            CalculateStaminaDamage(character);
        }

        void CalculateStaminaDamage(CharacterManager character) {
            if (character.IsOwner) {
                Debug.Log("character take: " + staminaDamage + "stamina dmg");
                character.GetCharacterNetworkManager().GetCurrentStamina().Value -= staminaDamage;
            }
        }

        internal void SetStaminaDamage(int amount) { staminaDamage = amount; }
    }
}