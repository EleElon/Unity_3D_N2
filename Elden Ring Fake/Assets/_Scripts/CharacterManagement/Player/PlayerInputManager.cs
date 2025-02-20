using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG {
    class PlayerInputManager : MonoBehaviour {

        internal static PlayerInputManager Instance { get; private set; }

        PlayerControls _playerControls;
        Vector2 movementInput;

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
    }
}
