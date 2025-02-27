using System;
using UnityEngine;

namespace SG {
    class CharacterStatManager : MonoBehaviour {

        CharacterManager _characterManager;

        [Header("---------- Stamina Regenaration ----------")]
        int staminaAmount = 2;
        float targetStamina;
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
            float currentStamina = _characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value;
            float maxStamina = _characterManager.GetCharacterNetworkManager().GetMaxStamina();

            if (!_characterManager.IsOwner)
                return;

            if (_characterManager.GetCharacterNetworkManager().GetIsSprinting())
                return;

            if (_characterManager.GetIsPerformingAction())
                return;

            staminaRegenatationTimer += Time.deltaTime;

            if (staminaRegenatationTimer >= staminaRegenerateDelay) {
                if (currentStamina < maxStamina) {
                    staminaTickTimer += Time.deltaTime;

                    if (staminaTickTimer >= 0.1) {
                        staminaTickTimer = 0;

                        targetStamina = Mathf.Min(currentStamina + staminaAmount, maxStamina);

                        _characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value = Mathf.Lerp(currentStamina, targetStamina, 1f);
                    }
                }
            }
        }

        //FIXME: fixed ease stamina bar
        internal virtual void ChangeEaseStamina() {
            if (targetStamina < _characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value) {
                _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value = Mathf.Lerp(_characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value, targetStamina, 0.1f);
            }
            else {
                _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value = targetStamina;
            }
        }

        internal virtual void ResetRegenerationTimer(float previousStaminaAmount, float currentStaminaAmount) {
            if (currentStaminaAmount < previousStaminaAmount) {
                staminaRegenatationTimer = 0;
            }
        }
    }
}
