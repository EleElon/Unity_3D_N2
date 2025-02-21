using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SG {
    class PlayerLocomotionManager : CharacterLocomotionManager {

        PlayerManager _playerManager;
        float verticalMovement, horizontalMovement;
        float moveAmount;
        Vector3 moveDirection;
        Vector3 targetRotationDirection;
        float walkingSpeed = 2, runningSpeed = 5, rotationSpeed = 15;

        protected override void Awake() {
            base.Awake();

            _playerManager = GetComponent<PlayerManager>();
        }

        protected override void Update() {
            base.Update();

            if (_playerManager.IsOwner) {
                _playerManager.GetCharacterNetworkManager().SetVerticalMovement(verticalMovement);
                _playerManager.GetCharacterNetworkManager().SetHorizontalMovement(horizontalMovement);
                _playerManager.GetCharacterNetworkManager().SetMoveAmount(moveAmount);
            }
            else {
                verticalMovement = _playerManager.GetCharacterNetworkManager().GetVerticalMovement();
                horizontalMovement = _playerManager.GetCharacterNetworkManager().GetHorizontalMovement();
                moveAmount = _playerManager.GetCharacterNetworkManager().GetMoveAmount();

                _playerManager.GetPlayerAnimatorManager().UpdateAnimatorMovementParameters(0, moveAmount);
            }
        }

        internal void HandleAllMovement() {
            HandleGroundedMovement();
            HandleRotation();
        }

        void GetMovementValues() {
            verticalMovement = PlayerInputManager.Instance.GetVerticalInput();
            horizontalMovement = PlayerInputManager.Instance.GetHorizontalInput();
            moveAmount = PlayerInputManager.Instance.GetMoveAmount();
        }

        void HandleGroundedMovement() {
            GetMovementValues();

            moveDirection = PlayerCameraManager.Instance.transform.forward * verticalMovement;
            moveDirection += PlayerCameraManager.Instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (PlayerInputManager.Instance.GetMoveAmount() > 0.5f) {
                _playerManager.GetCharacterController().Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.Instance.GetMoveAmount() <= 0.5f) {
                _playerManager.GetCharacterController().Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        void HandleRotation() {
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCameraManager.Instance.GetCamera().transform.forward * verticalMovement;
            targetRotationDirection += PlayerCameraManager.Instance.GetCamera().transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero) {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }
}
