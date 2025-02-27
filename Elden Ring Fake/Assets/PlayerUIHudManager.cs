using UnityEngine;

namespace SG {
    class PlayerUIHudManager : MonoBehaviour {

        [SerializeField] UI_StatBar staminaBar;

        internal void SetNewStaminaValue(float oldValue, float newValue) {
            staminaBar.SetStat(newValue);
        }

        internal void SetMaxStaminaValue(float maxValue) {
            staminaBar.SetMaxStat(maxValue);
        }
    }
}
