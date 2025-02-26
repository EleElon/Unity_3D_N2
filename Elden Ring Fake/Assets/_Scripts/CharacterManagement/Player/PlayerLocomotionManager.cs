using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SG {
    class PlayerLocomotionManager : CharacterLocomotionManager {

        PlayerManager _playerManager;
        float verticalMovement, horizontalMovement;
        float moveAmount;

        [Header("---------- Movement Settings ----------")]
        Vector3 moveDirection;
        Vector3 targetRotationDirection;
        float walkingSpeed = 2f, runningSpeed = 5f, sprintingSpeed = 6.5f, rotationSpeed = 15;

        [Header("---------- Dodge ----------")]
        Vector3 rollDirection;

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

                _playerManager.GetPlayerAnimatorManager().UpdateAnimatorMovementParameters(0, moveAmount, _playerManager.GetPlayerNetworkManager().GetIsSprinting());
            }
        }

        internal void HandleAllMovement() {
            if (_playerManager.GetIsPerformingAction())
                return;

            HandleGroundedMovement();
            HandleRotation();
        }

        void GetMovementValues() {
            verticalMovement = PlayerInputManager.Instance.GetVerticalInput();
            horizontalMovement = PlayerInputManager.Instance.GetHorizontalInput();
            moveAmount = PlayerInputManager.Instance.GetMoveAmount();
        }

        void HandleGroundedMovement() {
            if (!_playerManager.GetCanMove())
                return;

            GetMovementValues();

            moveDirection = PlayerCameraManager.Instance.transform.forward * verticalMovement;
            moveDirection += PlayerCameraManager.Instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (_playerManager.GetPlayerNetworkManager().GetIsSprinting()) {
                _playerManager.GetCharacterController().Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else {
                if (PlayerInputManager.Instance.GetMoveAmount() > 0.5f) {
                    _playerManager.GetCharacterController().Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.Instance.GetMoveAmount() <= 0.5f) {
                    _playerManager.GetCharacterController().Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }

        void HandleRotation() {
            if (!_playerManager.GetCanRotate())
                return;

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

        internal void HandleSprinting() {
            if (_playerManager.GetIsPerformingAction()) {
                _playerManager.GetPlayerNetworkManager().SetIsSprinting(false);
            }

            if (moveAmount >= 0.5) {
                _playerManager.GetPlayerNetworkManager().SetIsSprinting(true);
            }
            else {
                _playerManager.GetPlayerNetworkManager().SetIsSprinting(false);
            }
        }

        internal void AttemptToPerformDodge() {
            if (_playerManager.GetIsPerformingAction())
                return;

            if (PlayerInputManager.Instance.GetMoveAmount() > 0) {
                rollDirection = PlayerCameraManager.Instance.GetCamera().transform.forward * PlayerInputManager.Instance.GetVerticalInput();
                rollDirection += PlayerCameraManager.Instance.GetCamera().transform.right * PlayerInputManager.Instance.GetHorizontalInput();
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                _playerManager.transform.rotation = playerRotation;

                _playerManager.GetPlayerAnimatorManager().PlayTargetActionAnimation("Roll_Forward_01", true, true);
            }
            else {
                _playerManager.GetPlayerAnimatorManager().PlayTargetActionAnimation("Back_Step_01", true, true);
            }
        }
    }
}
