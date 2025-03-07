using UnityEngine;

namespace SG {
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    class TakeDamageEffect : InstantCharacterEffect {

        [Header("---------- Character Causing Damage ----------")]
        CharacterManager characterCausingDamage;

        [Header("---------- Damage ----------")]
        float physicalDamage, magicalDamage, fireDamage, lightningDamage, holyDamage;

        [Header("---------- Poise ----------")]
        float poiseDamage;
        bool poiseIsBroken = false;

        [Header("---------- Final Damage Dealt ----------")]
        float finalDamageDealt;

        [Header("---------- Animation ----------")]
        bool playDamageAnimation = true;
        bool manuallySelectDamageAnimation = false;
        string damageAnimation;

        [Header("---------- SFX ----------")]
        bool willPlayDamageSFX = true;
        [SerializeField] AudioClip elementalDamageSFX;

        [Header("---------- Direction Damage Taken From ----------")]
        float angleHitFrom;
        Vector3 contactPoint;

        internal override void ProcessEffect(CharacterManager character) {
            base.ProcessEffect(character);

            if (character.GetIsDead().Value)
                return;

            CalculateDamage(character);
        }

        void CalculateDamage(CharacterManager character) {
            if (!character.IsOwner)
                return;

            if (characterCausingDamage != null) {

            }

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicalDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0) {
                finalDamageDealt = 1;
            }

            character.GetCharacterNetworkManager().GetnSetCurrentHealth().Value -= finalDamageDealt;
        }

        internal void SetPhysicalDamage(float amount) { physicalDamage = amount; }

        internal void SetMagicalDamage(float amount) { magicalDamage = amount; }

        internal void SetFireDamage(float amount) { fireDamage = amount; }

        internal void SetLightingDamage(float amount) { lightningDamage = amount; }

        internal void SetHolyDamage(float amount) { holyDamage = amount; }

        internal void SetContactPoint(Vector3 point) { contactPoint = point; }
    }
}