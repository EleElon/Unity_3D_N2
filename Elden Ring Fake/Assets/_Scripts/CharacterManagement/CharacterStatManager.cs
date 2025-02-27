using UnityEngine;

namespace SG {
    class CharacterStatManager : MonoBehaviour {

        CharacterManager _characterManager;

        [Header("---------- Stamina Regenaration ----------")]
        int staminaAmount = 10;
        float staminaRegenatationTimer = 0;
        float staminaTickTimer;
        float staminaRegenerateDelay = 2;

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();
        }

        internal int CalculateStaminaBasedOnEnduranceLevel(int endurance) {
            float stamina = 0;

            stamina = endurance * 10;

            return Mathf.RoundToInt(stamina);
        }

        internal virtual void RegenerateStamina() {
            if (!_characterManager.IsOwner)
                return;

            if (_characterManager.GetCharacterNetworkManager().GetIsSprinting())
                return;

            if (_characterManager.GetIsPerformingAction())
                return;

            staminaRegenatationTimer += Time.deltaTime;

            if (staminaRegenatationTimer >= staminaRegenerateDelay) {
                if (_characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value < _characterManager.GetCharacterNetworkManager().GetMaxStamina()) {
                    staminaTickTimer += Time.deltaTime;

                    if (staminaTickTimer >= 0.1) {
                        staminaTickTimer = 0;

                        _characterManager.GetCharacterNetworkManager().SetCurrentStamina(_characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value + staminaAmount);
                    }
                }
            }
        }

        internal virtual void ResetRegenerationTimer(float previousStaminaAmount, float currentStaminaAmount) {
            if (currentStaminaAmount < previousStaminaAmount) {
                staminaRegenatationTimer = 0;
            }
        }
    }
}
