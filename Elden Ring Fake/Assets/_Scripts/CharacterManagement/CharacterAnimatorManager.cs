using UnityEngine;

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
    }
}
