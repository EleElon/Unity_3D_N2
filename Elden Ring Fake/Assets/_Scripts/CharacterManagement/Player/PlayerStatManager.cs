using UnityEngine;

namespace SG {
    class PlayerStatManager : CharacterStatManager {

        PlayerManager _playerManager;

        protected override void Awake() {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }

        protected override void Start() {
            base.Start();

            CalculateHealthBasedOnVitalityLevel(_playerManager.GetPlayerNetworkManager().GetnSetVitality().Value);
            CalculateStaminaBasedOnEnduranceLevel(_playerManager.GetPlayerNetworkManager().GetEndurace().Value);
        }
    }
}
