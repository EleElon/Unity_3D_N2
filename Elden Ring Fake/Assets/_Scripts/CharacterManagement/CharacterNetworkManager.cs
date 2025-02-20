using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterNetworkManager : NetworkBehaviour {

        [Header("--------- Position ---------")]
        NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        Vector3 networkPositionVelocity;
        float networkPositionSmoothTime = 0.1f;
        float networkRotationSmoothTime = 0.1f;

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
    }
}
