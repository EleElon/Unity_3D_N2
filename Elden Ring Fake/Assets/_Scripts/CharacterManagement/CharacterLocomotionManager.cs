using UnityEngine;

namespace SG {
    class CharacterLocomotionManager : MonoBehaviour {

        CharacterManager _characterManager;

        [Header("---------- Ground Check & Jumping ----------")]
        [SerializeField] LayerMask groundLayer;
        float groundCheckSphereRadius = 1;
        protected Vector3 yVelocity;
        float gravityForce = -5.55f;
        protected float groundedYVelocity = -20;
        protected float fallStartYVelocity = -5;
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer;

        protected virtual void Awake() {
            _characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update() {
            if (!WorldSaveGameManager.Instance.GetIsSceneLoaded())
                return;

            HandleGroundCheck();

            if (_characterManager.GetIsGrounded()) {
                if (yVelocity.y < 0) {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else {
                if (!_characterManager.GetIsJumping() && !fallingVelocityHasBeenSet) {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }
                inAirTimer += Time.deltaTime;
                _characterManager.GetAnimator().SetFloat("InAirTimer", inAirTimer);

                yVelocity.y += gravityForce * Time.deltaTime;
            }
            _characterManager.GetCharacterController().Move(yVelocity * Time.deltaTime);
        }

        void HandleGroundCheck() {
            _characterManager.SetIsGrounded(Physics.CheckSphere(_characterManager.transform.position, groundCheckSphereRadius, groundLayer));
        }

        void OnDrawGizmosSelected() {
            Gizmos.DrawSphere(_characterManager.transform.position, groundCheckSphereRadius);
        }
    }
}
