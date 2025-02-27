using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterManager : NetworkBehaviour {

        protected CharacterController _characterController;
        CharacterNetworkManager _characterNetworkManager;

        Animator _animator;

        [Header("---------- Flags ----------")]
        bool isPerformingAction = false;
        bool canRotate = true, canMove = true;
        bool applyRootMotion = false;

        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);

            _characterController = GetComponent<CharacterController>();
            _characterNetworkManager = GetComponent<CharacterNetworkManager>();

            _animator = GetComponent<Animator>();
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

        protected virtual void LateUpdate() {

        }   

        internal CharacterController GetCharacterController() {
            return _characterController;
        }

        internal CharacterNetworkManager GetCharacterNetworkManager() {
            return _characterNetworkManager;
        }

        internal Animator GetAnimator() {
            return _animator;
        }

        internal bool GetIsPerformingAction() {
            return isPerformingAction;
        }

        internal void SetIsPerformingAction(bool state) {
            isPerformingAction = state;
        }

        internal bool GetCanMove() {
            return canMove;
        }

        internal void SetCanMove(bool state) {
            canMove = state;
        }

        internal bool GetCanRotate() {
            return canRotate;
        }

        internal void SetCanRotate(bool state) {
            canRotate = state;
        }

        internal bool GetApplyRootMotion() {
            return applyRootMotion;
        }

        internal void SetApplyRootMotion(bool state) {
            applyRootMotion = state;
        }
    }
}
