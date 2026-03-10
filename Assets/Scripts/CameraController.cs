using UnityEngine;

public class CameraController : MonoBehaviour {


    [Header("Camera Attributes")]
    [SerializeField] private float mouseSensitivity = 1f;

    [Header("References")]
    [SerializeField] private Transform playerTransform;

    
    private float verticalRotation;
    private float verticalRotationLimit = 60f;


    private void Awake() {
        verticalRotation = this.transform.rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
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


}
