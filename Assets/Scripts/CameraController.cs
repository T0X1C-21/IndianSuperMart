using UnityEngine;

public class CameraController : MonoBehaviour {


    public static CameraController Instance { get; private set; }


    [Header("Camera Attributes")]
    [SerializeField] private float mouseSensitivity = 1f;

    [Header("References")]
    [SerializeField] private Transform playerTransform;

    
    private bool canMove = true;
    private float verticalRotation;
    private float verticalRotationLimit = 80f;


    private void Awake() {
        Instance = this;

        verticalRotation = this.transform.rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() {
        if (!canMove) {
            return;
        }
        Look();
    }

    private void Look() {
        Vector2 lookInputVector = GameInput.Instance.GetLookInputVector();

        float xRotation = lookInputVector.x * mouseSensitivity * Time.deltaTime;
        float yRotation = lookInputVector.y * mouseSensitivity * Time.deltaTime;

        // Horizontal Rotation
        playerTransform.Rotate(0f, xRotation, 0f);

        // Vertical Rotation
        verticalRotation -= yRotation;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        this.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    public void EnableMovement() {
        canMove = true;
    }

    public void DisableMovement() {
        canMove = false;
    }


}
