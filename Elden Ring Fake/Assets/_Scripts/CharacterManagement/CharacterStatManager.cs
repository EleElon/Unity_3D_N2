using System;
using UnityEngine;

namespace SG {
    class CharacterStatManager : MonoBehaviour {

        CharacterManager _characterManager;

        [Header("---------- Stamina Regenaration ----------")]
        int staminaAmount = 2;
        float targetStamina;
        float staminaRegenatationTimer = 0, easeStaminaTimer;
        float staminaTickTimer;
        float staminaRegenerateDelay = 2, easeStaminaDelay = 1;

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

            OutFunctionCalculateStamina();

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

        internal virtual void UsingStamina() {
            
        }

        internal virtual void ChangeEaseStamina() {
            float currentStamina = _characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value;
            float currentEaseStamina = _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value;

            OutFunctionCalculateStamina();

            if (easeStaminaTimer >= easeStaminaDelay) {
                if (currentStamina < currentEaseStamina) {
                    _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value = Mathf.Lerp(currentEaseStamina, currentStamina, 0.1f);
                }
            }

            if (currentEaseStamina < currentStamina) {
                _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value = currentStamina;
            }
        }

        internal virtual void ResetRegenerationTimer(float previousStaminaAmount, float currentStaminaAmount) {
            if (currentStaminaAmount < previousStaminaAmount) {
                staminaRegenatationTimer = 0;
            }
        }

        internal virtual void SetEaseTimer() {
            if (!_characterManager.IsOwner) {
                easeStaminaTimer = 0;
                return;
            }

            if (_characterManager.GetCharacterNetworkManager().GetIsSprinting()) {
                easeStaminaTimer = 0;
                return;
            }

            if (_characterManager.GetIsPerformingAction()) {
                easeStaminaTimer = 0;
                return;
            }

            if (_characterManager.GetCharacterNetworkManager().GetCurrentStamina().Value < _characterManager.GetCharacterNetworkManager().GetCurrentEaseStamina().Value) {
                easeStaminaTimer += Time.deltaTime;
            }
            else {
                easeStaminaTimer = 0;
            }
        }

        void OutFunctionCalculateStamina() {
            if (!_characterManager.IsOwner)
                return;

            if (_characterManager.GetCharacterNetworkManager().GetIsSprinting())
                return;

            if (_characterManager.GetIsPerformingAction())
                return;
        }
    }
}
