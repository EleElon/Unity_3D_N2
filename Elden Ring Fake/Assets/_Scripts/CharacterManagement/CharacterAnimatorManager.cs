using UnityEngine;
using Unity.Netcode;

namespace SG {
    class CharacterAnimatorManager : MonoBehaviour {

        CharacterManager _characterManager;

        float horizontal, vertical;

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();
        }

        internal void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue) {
            _characterManager.GetAnimator().SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            _characterManager.GetAnimator().SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
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
