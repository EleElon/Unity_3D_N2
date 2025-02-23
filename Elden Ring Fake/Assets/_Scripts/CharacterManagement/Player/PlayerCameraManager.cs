using UnityEngine;

namespace SG {
    class PlayerCameraManager : MonoBehaviour {

        internal static PlayerCameraManager Instance { get; private set; }

        PlayerManager _playerManager;
        [SerializeField] Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("---------- Camera Settings ----------")]
        float cameraSmoothSpeed = 0.1f;
        float leftAndRightRotationSpeed = 220f, upAndDownRotationSpeed = 220f;
        float minimumPivot = -30, maximumPivot = 60;
        float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;

        [Header("---------- Camera Values ----------")]
        Vector3 cameraVelocity;
        Vector3 cameraObjectPosition;
        float leftAndRightLookAngle, upAndDownLookAngle;
        float cameraZPosition;
        float targetCameraZPosition;

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

            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        internal Camera GetCamera() {
            return cameraObject;
        }

        internal void HandleAllCameraActions() {
            if (_playerManager != null) {
                HandleFollowTarget();
                HandleRotation();
                HandleCollisions();
            }
        }

        void HandleFollowTarget() {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, _playerManager.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        void HandleRotation() {
            leftAndRightLookAngle += (PlayerInputManager.Instance.GetCameraHorizontalInput() * leftAndRightRotationSpeed) * Time.deltaTime;
            upAndDownLookAngle -= (PlayerInputManager.Instance.GetCameraVerticalInput() * upAndDownRotationSpeed) * Time.deltaTime;

            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            // transform.rotation = targetRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            // cameraPivotTransform.localRotation = targetRotation;
            cameraPivotTransform.localRotation = Quaternion.Slerp(cameraPivotTransform.localRotation, targetRotation, Time.deltaTime * 2f);
        }

        void HandleCollisions() {
            targetCameraZPosition = cameraZPosition;

            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers)) {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius) {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, cameraCollisionRadius);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }

        internal void SetPlayerManager(PlayerManager playerManager) {
            _playerManager = playerManager;
        }
    }
}
