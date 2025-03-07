using System.Collections.Generic;
using UnityEngine;

namespace SG {
    class DamagedCollider : MonoBehaviour {

        [Header("---------- Damage ----------")]
        float physicalDamage = 25, magicalDamage, fireDamage, lightningDamage, holyDamage;

        [Header("---------- Contact Point ----------")]
        protected Vector3 contactPoint;

        [Header("---------- Characters Damaged ----------")]
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        void OnTriggerEnter(Collider other) {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

            if (damageTarget != null) {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                DamagedTarget(damageTarget);
            }
        }

        protected virtual void DamagedTarget(CharacterManager damageTarget) {
            if (charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.Instance.GetTakeDamageEffect());

            damageEffect.SetPhysicalDamage(physicalDamage);
            damageEffect.SetMagicalDamage(magicalDamage);
            damageEffect.SetFireDamage(fireDamage);
            damageEffect.SetLightingDamage(lightningDamage);
            damageEffect.SetHolyDamage(holyDamage);
            damageEffect.SetContactPoint(contactPoint);

            damageTarget.GetCharacterEffectManager().ProcessInstantEffect(damageEffect);
        }
    }
}