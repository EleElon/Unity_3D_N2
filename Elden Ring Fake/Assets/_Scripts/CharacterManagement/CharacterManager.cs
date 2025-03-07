using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterManager : NetworkBehaviour {

        [Header("---------- Status ----------")]
        NetworkVariable<bool> isDead = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        protected CharacterController _characterController;
        CharacterNetworkManager _characterNetworkManager;
        CharacterEffectManager _characterEffectManager;

        Animator _animator;

        [Header("---------- Flags ----------")]
        bool isPerformingAction = false;
        bool canRotate = true, canMove = true, isJumping = false, isGrounded = true;
        bool applyRootMotion = false;

        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);

            _characterController = GetComponent<CharacterController>();
            _characterNetworkManager = GetComponent<CharacterNetworkManager>();
            _characterEffectManager = GetComponent<CharacterEffectManager>();

            _animator = GetComponent<Animator>();
        }

        protected virtual void Update() {
            _animator.SetBool("IsGrounded", isGrounded);

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

        internal CharacterController GetCharacterController() { return _characterController; }

        internal CharacterNetworkManager GetCharacterNetworkManager() { return _characterNetworkManager; }

        internal CharacterEffectManager GetCharacterEffectManager() { return _characterEffectManager; }

        internal Animator GetAnimator() { return _animator; }

        internal bool GetIsPerformingAction() { return isPerformingAction; }

        internal void SetIsPerformingAction(bool state) { isPerformingAction = state; }

        internal bool GetCanMove() { return canMove; }

        internal void SetCanMove(bool state) { canMove = state; }

        internal bool GetCanRotate() { return canRotate; }

        internal void SetCanRotate(bool state) { canRotate = state; }

        internal bool GetApplyRootMotion() { return applyRootMotion; }

        internal void SetApplyRootMotion(bool state) { applyRootMotion = state; }

        internal bool GetIsJumping() { return isJumping; }

        internal void SetIsJumping(bool state) { isJumping = state; }

        internal bool GetIsGrounded() { return isGrounded; }

        internal void SetIsGrounded(bool state) { isGrounded = state; }

        internal NetworkVariable<bool> GetIsDead() { return isDead; }
    }
}
