using System;
using Unity.Cinemachine;
using UnityEngine;

public class BillingDeskInteractable : MonoBehaviour, IInteractableObject {


    public event EventHandler OnBillingDeskEquipped;
    public event EventHandler OnBillingDeskUnequipped;


    [SerializeField] private GameObject attachedCamera;
    [SerializeField] private CinemachinePanTilt cinemachinePanTilt;
    [SerializeField] private new BoxCollider collider;


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
        if (isBillingDeskEnabled) {
            return;
        }

        attachedCamera.SetActive(true);
        isBillingDeskEnabled = true;
        //collider.enabled = false;

        OnBillingDeskEquipped?.Invoke(this, EventArgs.Empty);
    }

    private void DisableBillingDesk() {
        attachedCamera.SetActive(false);
        isBillingDeskEnabled = false;
        cinemachinePanTilt.PanAxis.TriggerRecentering();
        cinemachinePanTilt.TiltAxis.TriggerRecentering();
        //collider.enabled = true;

        OnBillingDeskUnequipped?.Invoke(this, EventArgs.Empty);
    }


}
