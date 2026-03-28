using UnityEngine;

public class PlayerController : MonoBehaviour {


    public static PlayerController Instance { get; private set; }


    [Header("Movement Attributes")]
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float gravityValue = -9.8f;


    private CharacterController characterController;
    private bool canMove = true;


    private void Awake() {
        Instance = this;

        characterController = this.GetComponent<CharacterController>();
        characterController.enableOverlapRecovery = false;
    }

    private void Update() {
        Run();
    }

    private void Run() {
        if (!canMove) {
            return;
        }

        // Get Input from GameInputClass
        Vector2 inputVector_Normalized = GameInput.Instance.GetRunInputVector();

        if(inputVector_Normalized == Vector2.zero) {
            return;
        }

        // Convert Input to Frame Independent Movement Vector
        Vector3 movementVector = new Vector3(inputVector_Normalized.x, gravityValue, inputVector_Normalized.y) * runSpeed * 
            Time.deltaTime;

        // Convert Movement Vector to Relative to Player
        Vector3 movementVectorRelativeToPlayer = (this.transform.right * movementVector.x) + 
            (this.transform.up * movementVector.y) + (this.transform.forward * movementVector.z);

        characterController.Move(movementVectorRelativeToPlayer);
    }

    public void EnableMovement() {
        canMove = true;
    }

    public void DisableMovement() {
        canMove = false;
    }


}
