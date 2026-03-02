using UnityEngine;

public class PlayerController : MonoBehaviour {


    [Header("Run Attributes")]
    [SerializeField] private float runSpeed = 1f;


    private void Update() {
        Run();
    }

    private void Run() {
        Vector2 inputVector_Normalized = GameInput.Instance.GetRunInputVector();
        if(inputVector_Normalized == Vector2.zero) {
            return;
        }

        Vector3 movementVector = new Vector3(inputVector_Normalized.x, 0f, inputVector_Normalized.y) * runSpeed * Time.deltaTime;

        this.transform.position += (this.transform.right * movementVector.x) + (this.transform.forward * movementVector.z);
    }


}
