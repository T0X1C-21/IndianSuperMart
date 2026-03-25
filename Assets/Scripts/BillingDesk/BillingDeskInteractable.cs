using Unity.Cinemachine;
using UnityEngine;

public class BillingDeskInteractable : MonoBehaviour, IInteractableObject {


    [SerializeField] private GameObject attachedCamera;
    [SerializeField] private CinemachinePanTilt cinemachinePanTilt;


    private GameInput gameInput;
    private bool isBillingDeskEnabled;


    private void Awake() {
        attachedCamera.SetActive(false);
    }

    private void Start() {
        gameInput = GameInput.Instance;
    }

    private void Update() {
        if (gameInput.GetCancelBool() && isBillingDeskEnabled) {
            DisableBillingDesk();
        }
    }

    public void Interact() {
        EnableBillingDesk();
    }

    private void EnableBillingDesk() {
        attachedCamera.SetActive(true);
        isBillingDeskEnabled = true;
    }

    private void DisableBillingDesk() {
        attachedCamera.SetActive(false);
        isBillingDeskEnabled = false;
        cinemachinePanTilt.PanAxis.TriggerRecentering();
        cinemachinePanTilt.TiltAxis.TriggerRecentering();
    }


}
