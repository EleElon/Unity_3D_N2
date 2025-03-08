using UnityEngine;

namespace SG {
    class PlayerUIHudManager : MonoBehaviour {

        [Header("---------- Health Bar ----------")]
        [SerializeField] UI_StatBar healthBar;
        [SerializeField] UI_StatBar easeHealthBar;

        [Header("---------- Stamina Bar ----------")]
        [SerializeField] UI_StatBar staminaBar;
        [SerializeField] UI_StatBar easeStaminaBar;

        internal void RefreshHUD() {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);

            easeHealthBar.gameObject.SetActive(false);
            easeHealthBar.gameObject.SetActive(true);

            staminaBar.gameObject.SetActive(false);
            staminaBar.gameObject.SetActive(true);

            easeStaminaBar.gameObject.SetActive(false);
            easeStaminaBar.gameObject.SetActive(true);
        }

        internal void SetNewHealthValue(int oldValue, int newValue) {
            healthBar.SetStat(newValue);
        }

        internal void SetMaxHealthValue(float maxValue) {
            healthBar.SetMaxStat(maxValue);
        }

        internal void SetNewEaseHealthValue(float oldValue, float newValue) {
            easeHealthBar.SetStat(newValue);
        }

        internal void SetMaxEaseHealthValue(float maxValue) {
            easeHealthBar.SetMaxStat(maxValue);
        }

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
