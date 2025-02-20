using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterManager : NetworkBehaviour {

        protected CharacterController _characterController;
        CharacterNetworkManager _characterNetworkManager;

        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);

            _characterController = GetComponent<CharacterController>();
            _characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update() {
            if (IsOwner) {
                _characterNetworkManager.SetNetworkPosition(transform.position);
                _characterNetworkManager.SetNetworkRotation(transform.rotation);
            }
            else {
                Vector3 velocity = _characterNetworkManager.GetNetworkPositionVelocity();
                transform.position = Vector3.SmoothDamp(transform.position, _characterNetworkManager.GetNetworkPosition(), ref velocity, _characterNetworkManager.GetNetworkPositionSmoothTime());

                transform.rotation = Quaternion.Slerp(transform.rotation, _characterNetworkManager.GetNetworkRotation(), _characterNetworkManager.GetNetworkRotationSmoothTime());
            }
        }

        internal CharacterController GetCharacterController() {
            return _characterController;
        }
    }
}
