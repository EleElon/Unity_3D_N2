using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterNetworkManager : NetworkBehaviour {

        CharacterManager _characterManager;

        [Header("--------- Position ---------")]
        NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        Vector3 networkPositionVelocity;
        float networkPositionSmoothTime = 0.1f;
        float networkRotationSmoothTime = 0.1f;

        [Header("--------- Animator ----------")]
        NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("---------- Flags ----------")]
        NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("---------- Stats ----------")]
        public NetworkVariable<int> endurance = new NetworkVariable<int>(10, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentEaseStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxStamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();
        }

        [ServerRpc]
        internal void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion) {
            if (IsServer) {
                PlayActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        [ClientRpc]
        void PlayActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool applyRootMotion) {
            if (clientID != NetworkManager.Singleton.LocalClientId) {
                PerformActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        void PerformActionAnimationFromServer(string animationID, bool applyRootMotion) {
            _characterManager.SetApplyRootMotion(applyRootMotion);
            _characterManager.GetAnimator().CrossFade(animationID, 0.2f);
        }

        internal Vector3 GetNetworkPositionVelocity() {
            return networkPositionVelocity;
        }

        internal float GetNetworkPositionSmoothTime() {
            return networkPositionSmoothTime;
        }

        internal float GetNetworkRotationSmoothTime() {
            return networkRotationSmoothTime;
        }

        internal void SetNetworkPosition(Vector3 newPosition) {
            networkPosition.Value = newPosition;
        }

        internal Vector3 GetNetworkPosition() {
            return networkPosition.Value;
        }

        internal void SetNetworkRotation(Quaternion newRotation) {
            networkRotation.Value = newRotation;
        }

        internal Quaternion GetNetworkRotation() {
            return networkRotation.Value;
        }

        internal float SetHorizontalMovement(float movement) {
            return horizontalMovement.Value = movement;
        }

        internal float GetHorizontalMovement() {
            return horizontalMovement.Value;
        }

        internal float SetVerticalMovement(float movement) {
            return verticalMovement.Value = movement;
        }

        internal float GetVerticalMovement() {
            return verticalMovement.Value;
        }

        internal float SetMoveAmount(float amount) {
            return moveAmount.Value = amount;
        }

        internal float GetMoveAmount() {
            return moveAmount.Value;
        }

        internal bool GetIsSprinting() {
            return isSprinting.Value;
        }

        internal void SetIsSprinting(bool state) {
            isSprinting.Value = state;
        }

        internal int GetEndurace() {
            return endurance.Value;
        }

        internal NetworkVariable<float> GetCurrentStamina() {
            return currentStamina;
        }

        internal void SetCurrentStamina(float newValue) {
            currentStamina.Value = newValue;
        }

        internal NetworkVariable<float> GetCurrentEaseStamina() {
            return currentEaseStamina;
        }

        internal int GetMaxStamina() {
            return maxStamina.Value;
        }

        internal void SetMaxStamina(int maxValue) {
            maxStamina.Value = maxValue;
        }
    }
}
