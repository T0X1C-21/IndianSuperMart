using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillingDesk : MonoBehaviour {


    public event EventHandler<OnStartScanModeEventArgs> OnStartScanMode;
    public class OnStartScanModeEventArgs : EventArgs {
        public int receivedAmount;
    }
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
    [SerializeField] private GameObject productSummaryTemplateGameObject;
    [SerializeField] private Transform listOfSummariesTransform;
    [SerializeField] private TextMeshProUGUI totalPriceTMP;


    private State state;
    private State previousState;
    private bool doOnce = true;
    private List<ProductSummaryTemplate> spawnedProductSummaryTemplateList = new List<ProductSummaryTemplate>();
    private int totalPrice = 0;


    private void Awake() {
        state = State.Dead;
    }

    private void Start() {
        billingDeskItemSlotsManager.OnAllItemsScanned += BillingDeskItemSlotsManager_OnAllItemsScanned;

        billingDeskInteractable.OnBillingDeskEquipped += BillingDeskInteractable_OnBillingDeskEquipped;
        billingDeskInteractable.OnBillingDeskUnequipped += BillingDeskInteractable_OnBillingDeskUnequipped;
    }

    private void BillingDeskInteractable_OnBillingDeskUnequipped(object sender, EventArgs e) { 
        PlayerController.Instance.EnableMovement();
        CameraController.Instance.EnableMovement();

        previousState = state;
        ChangeState(State.Dead);
    }

    private void BillingDeskInteractable_OnBillingDeskEquipped(object sender, EventArgs e) {
        if (doOnce && itemArray.Length > 0) {
            AddItemsToBillingDesk(itemArray);
            doOnce = false;
        }

        PlayerController.Instance.DisableMovement();
        CameraController.Instance.DisableMovement();

        if(previousState == State.Scan || previousState == State.Payment) {
            ChangeState(previousState);
            OnStartScanMode?.Invoke(this, new OnStartScanModeEventArgs {
                receivedAmount = 800
            });
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
        AddToBillingDeskScreenUI();
    }

    private void AddToBillingDeskScreenUI() { 
        HashSet<ItemSO> itemHashSet = new HashSet<ItemSO>();
        foreach(Item item in itemArray) {
            ItemSO itemSO = item.GetItemSO();
            if (!itemHashSet.Contains(itemSO)) {
                itemHashSet.Add(itemSO);
                AddToProductSummaryUI(itemSO);
            }
        }
    }

    private void AddToProductSummaryUI(ItemSO itemSO) {
        int count = 0;
        foreach(Item item in itemArray) {
            if(item.GetItemSO().name == itemSO.name) {
                count++;
            }
        }

        GameObject spawnedProductSummaryTemplateGameObject = Instantiate(productSummaryTemplateGameObject, 
            listOfSummariesTransform);
        ProductSummaryTemplate spawnedProductSummaryTemplate = 
            spawnedProductSummaryTemplateGameObject.GetComponent<ProductSummaryTemplate>();
        spawnedProductSummaryTemplate.SetParametersAndReturnTotal(itemSO, count, totalPrice, out totalPrice);
        totalPriceTMP.text = "₹" + totalPrice.ToString();
        spawnedProductSummaryTemplateList.Add(spawnedProductSummaryTemplate);
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
                OnStartScanMode?.Invoke(this, new OnStartScanModeEventArgs {
                    receivedAmount = 800
                });
                state = newState;
                break;
            case State.Payment:
                state = newState;
                OnStartPaymentMode?.Invoke(this, EventArgs.Empty);
                break;
        }
    }


}
