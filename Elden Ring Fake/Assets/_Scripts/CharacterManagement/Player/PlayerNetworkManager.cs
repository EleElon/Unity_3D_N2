using UnityEngine;
using Unity.Netcode;
using System.Collections;
using Unity.Collections;

namespace SG {
    class PlayerNetworkManager : CharacterNetworkManager {

        PlayerManager _playerManager;

        NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected override void Awake() {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }

        internal void SetNewMaxHealthValue(int oldVitality, int newVitality) {
            GetnSetMaxHealth().Value = _playerManager.GetPlayerStatManager().CalculateHealthBasedOnVitalityLevel(newVitality);
            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxHealthValue(GetnSetMaxHealth().Value);
            GetnSetCurrentHealth().Value = GetnSetMaxHealth().Value;
            Debug.Log("changed hp");
        }

        internal void SetNewMaxStaminaValue(int oldEndurance, int newEndurance) {
            SetMaxStamina(_playerManager.GetPlayerStatManager().CalculateStaminaBasedOnEnduranceLevel(newEndurance));
            PlayerUIManager.Instance.GetPlayerUIHudManager().SetMaxStaminaValue(GetMaxStamina());
            SetCurrentStamina(GetMaxStamina());
            Debug.Log("changed stamina");
        }

        internal NetworkVariable<FixedString64Bytes> GetnSetCharacterName() { return characterName; }
    }
}
