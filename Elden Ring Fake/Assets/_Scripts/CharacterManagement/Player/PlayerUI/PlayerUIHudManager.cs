using UnityEngine;

namespace SG {
    class PlayerUIHudManager : MonoBehaviour {

        [SerializeField] UI_StatBar staminaBar;
        [SerializeField] UI_StatBar easeStaminaBar;

        internal void SetNewStaminaValue(float oldValue, float newValue) {
            staminaBar.SetStat(newValue);
        }

        internal void SetMaxStaminaValue(float maxValue) {
            staminaBar.SetMaxStat(maxValue);
        }

        internal void SetNewEaseStaminaValue(float oldValue, float newValue) {
            easeStaminaBar.SetStat(newValue);
        }

        internal void SetMaxEaseStaminaValue(float maxValue) {
            easeStaminaBar.SetMaxStat(maxValue);
        }
    }
}
