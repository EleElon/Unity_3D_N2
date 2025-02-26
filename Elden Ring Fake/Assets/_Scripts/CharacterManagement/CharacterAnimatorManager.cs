using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterAnimatorManager : MonoBehaviour {

        CharacterManager _characterManager;

        int horizontal, vertical;

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        internal void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting) {
            float verticalAmount = verticalValue;
            float horizontalAmount = horizontalValue;

            if (isSprinting) {
                verticalAmount = 2;
            }

            _characterManager.GetAnimator().SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            _characterManager.GetAnimator().SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        internal void PlayTargetActionAnimation(string targetAnimation, bool isPerfomingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false) {
            _characterManager.SetApplyRootMotion(applyRootMotion);
            _characterManager.GetAnimator().CrossFade(targetAnimation, 0.2f);
            _characterManager.SetIsPerformingAction(isPerfomingAction);
            _characterManager.SetCanMove(canMove);
            _characterManager.SetCanRotate(canRotate);

            _characterManager.GetCharacterNetworkManager().NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }
    }
}
