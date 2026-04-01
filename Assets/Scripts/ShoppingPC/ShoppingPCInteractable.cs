using UnityEngine;

public class ShoppingPCInteractable : MonoBehaviour, IInteractableObject {


    [SerializeField] private GameObject attachedCamera;
    [SerializeField] private new BoxCollider collider;


    private GameInput gameInput;
    private Player player;
    private bool isShoppingPCEnabled;


    private void Awake() {
        attachedCamera.SetActive(false);
    }

    private void Start() {
        gameInput = GameInput.Instance;
        player = Player.Instance;
    }

    private void Update() {
        if (gameInput.GetCancelBool() && isShoppingPCEnabled) {
            DisableShoppingPC();
        }
    }

    public void Interact() {
        float dotProductBetweenPlayerAndShoppingPC = Vector3.Dot(this.transform.forward, player.transform.forward);

        float interactionThreshold = -0.5f;
        if(dotProductBetweenPlayerAndShoppingPC < interactionThreshold) {
            EnableShoppingPC();
        }
    }

    private void EnableShoppingPC() {
        if (isShoppingPCEnabled) {
            return;
        }

        attachedCamera.SetActive(true);
        isShoppingPCEnabled = true;
        this.collider.enabled = false;

        UIManager.Instance.DisableCrosshair();
    }

    private void DisableShoppingPC() {
        attachedCamera.SetActive(false);
        isShoppingPCEnabled = false;
        this.collider.enabled = true;

        UIManager.Instance.EnableCrosshair();
    }
    
}
