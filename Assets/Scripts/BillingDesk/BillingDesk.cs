using System;
using UnityEngine;

public class BillingDesk : MonoBehaviour {


    public event EventHandler OnStartPaymentMode;
    public event EventHandler OnEndPaymentMode;


    public enum State {
        Dead,
        Idle,
        Scan,
        Payment
    }


    [SerializeField] private BillingDeskInteractable billingDeskInteractable;
    [SerializeField] private BillingDeskItemSlotsManager billingDeskItemSlotsManager;
    [SerializeField] private Item[] itemArray;


    [SerializeField] private State state;
    private State previousState;


    private void Awake() {
        state = State.Dead;
    }

    private void Start() {
        if(itemArray.Length > 0) {
            AddItemsToBillingDesk(itemArray);
        }

        billingDeskItemSlotsManager.OnAllItemsScanned += BillingDeskItemSlotsManager_OnAllItemsScanned;

        billingDeskInteractable.OnBillingDeskEquipped += BillingDeskInteractable_OnBillingDeskEquipped;
        billingDeskInteractable.OnBillingDeskUnequipped += BillingDeskInteractable_OnBillingDeskUnequipped;
    }

    private void BillingDeskInteractable_OnBillingDeskUnequipped(object sender, EventArgs e) { 
        PlayerController.Instance.EnableMovement();

        previousState = state;
        ChangeState(State.Dead);
    }

    private void BillingDeskInteractable_OnBillingDeskEquipped(object sender, EventArgs e) {
        PlayerController.Instance.DisableMovement();

        if(previousState == State.Scan || previousState == State.Payment) {
            ChangeState(previousState);
            return;
        } 
        ChangeState(State.Idle);
    }

    private void BillingDeskItemSlotsManager_OnAllItemsScanned(object sender, System.EventArgs e) {
        ChangeState(State.Payment);
    }

    public void AddItemsToBillingDesk(Item[] itemArray) {
        ChangeState(State.Scan);
        billingDeskItemSlotsManager.AddItemsToInputSlots(itemArray);
    }

    private void ChangeState(State newState) {
        switch (newState) {
            case State.Dead:
                billingDeskItemSlotsManager.DisableAllInteractions();
                state = newState;

                OnEndPaymentMode?.Invoke(this, EventArgs.Empty);
                break;
            case State.Idle:
                billingDeskItemSlotsManager.EnableAllInteractions();
                state = newState;
                break;
            case State.Scan:
                if(state != State.Idle) {
                    billingDeskItemSlotsManager.EnableAllInteractions();
                }
                state = newState;
                break;
            case State.Payment:
                state = newState;
                OnStartPaymentMode?.Invoke(this, EventArgs.Empty);
                break;
        }
    }


}
