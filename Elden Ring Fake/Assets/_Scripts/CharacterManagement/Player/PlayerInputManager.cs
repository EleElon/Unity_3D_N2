using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG {
    class PlayerInputManager : MonoBehaviour {

        internal static PlayerInputManager Instance { get; private set; }

        PlayerControls _playerControls;
        [SerializeField] Vector2 movementInput;
        [SerializeField] float verticalInput, horizontalInput;
        [SerializeField] float moveAmount;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        void Start() {
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += OnScreenChanged;

            Instance.enabled = false;
        }

        void Update() {
            HandleMovementInput();
        }

        private void OnScreenChanged(Scene oldScene, Scene newScene) {
            if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex()) {
                Instance.enabled = true;
            }
            else {
                Instance.enabled = false;
            }
        }

        void OnEnable() {
            if (_playerControls == null) {
                _playerControls = new PlayerControls();

                _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            _playerControls.Enable();
        }

        void OnDestroy() {
            SceneManager.activeSceneChanged -= OnScreenChanged;
        }

        void OnApplicationFocus(bool focus) {
            if (enabled) {
                if (focus) {
                    _playerControls.Enable();
                }
                else {
                    _playerControls.Disable();
                }
            }
        }

        void HandleMovementInput() {
            if (_playerControls != null) {
                verticalInput = movementInput.y;
                horizontalInput = movementInput.x;
            }
            else {
                verticalInput = Input.GetAxisRaw("Vertical");
                horizontalInput = Input.GetAxisRaw("Horizontal");
            }

            moveAmount = Mathf.Clamp01(MathF.Abs(verticalInput) + MathF.Abs(horizontalInput));

            if (moveAmount <= 0.5 && moveAmount > 0) {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1) {
                moveAmount = 1;
            }
        }

        internal float GetPlayerMoveAmount() {
            return moveAmount;
        }

        internal float GetVerticalInput() {
            return verticalInput;
        }

        internal float GetHorizontalInput() {
            return horizontalInput;
        }
    }
}
