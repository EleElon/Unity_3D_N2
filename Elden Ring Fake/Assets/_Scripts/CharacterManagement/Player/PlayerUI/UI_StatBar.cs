using UnityEngine;
using UnityEngine.UI;

namespace SG {
    class UI_StatBar : MonoBehaviour {

        Slider slider;
        RectTransform rectTransform;

        [Header("---------- Bar Option ----------")]
        [SerializeField] bool scaleBarLengthWithStat = true;
        [SerializeField] float withScaleMultiplier = 1;

        protected virtual void Awake() {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }

        internal virtual void SetStat(float newValue) {
            slider.value = newValue;
        }

        internal virtual void SetMaxStat(float maxValue) {
            slider.maxValue = maxValue;
            slider.value = maxValue;

            if (scaleBarLengthWithStat) {
                rectTransform.sizeDelta = new Vector2(maxValue * withScaleMultiplier, rectTransform.sizeDelta.y);

                PlayerUIManager.Instance.GetPlayerUIHudManager().RefreshHUD();
            }
        }
    }
}