using UnityEngine;
using UnityEngine.UI;

namespace SG {
    class UI_StatBar : MonoBehaviour {

        Slider slider;

        protected virtual void Awake() {
            slider = GetComponentInChildren<Slider>();
        }

        internal virtual void SetStat(float newValue) {
            slider.value = newValue;
        }

        internal virtual void SetMaxStat(float maxValue) {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
    }
}