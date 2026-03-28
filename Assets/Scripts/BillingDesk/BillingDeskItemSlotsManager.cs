using System;
using UnityEngine;

public class BillingDeskItemSlotsManager : MonoBehaviour {


    public event EventHandler OnAllItemsScanned;


    [SerializeField] private BillingDeskInputSlot[] billingDeskInputSlots;
    [SerializeField] private BillingDeskOutputSlot[] billingDeskOutputSlots;


    private int freeOutputSlotIndex;
    private int remainingItemsToScan;


    public void AddItemsToInputSlots(Item[] itemArray) {
        remainingItemsToScan = itemArray.Length;

        for(int i = 0; i < itemArray.Length; i++) {
            billingDeskInputSlots[i].AddItemToSelf(itemArray[i]);
        }
    }

    public BillingDeskOutputSlot GetNextFreeOutputSlot() {
        if(freeOutputSlotIndex + 1 == billingDeskOutputSlots.Length) {
            freeOutputSlotIndex = 0;
        }

        BillingDeskOutputSlot freeBillingDeskOutputSlot = billingDeskOutputSlots[freeOutputSlotIndex++];

        if(freeBillingDeskOutputSlot.GetIsContainingItem() == true) {
            Debug.LogError($"{freeBillingDeskOutputSlot.transform.name} contains an Item!");
            return null;
        }

        freeBillingDeskOutputSlot.SetIsContainingItem(true);
        return freeBillingDeskOutputSlot;
    }

    public void DecreaseRemainingItemsToScan() {
        remainingItemsToScan -= 1;
        if(remainingItemsToScan == 0) {
            // Change state to Payment
            OnAllItemsScanned?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DisableAllInteractions() {
        foreach(BillingDeskInputSlot billingDeskInputSlot in billingDeskInputSlots) {
            billingDeskInputSlot.SetCanInteract(false);
        }
    }

    public void EnableAllInteractions() {
        foreach(BillingDeskInputSlot billingDeskInputSlot in billingDeskInputSlots) {
            billingDeskInputSlot.SetCanInteract(true);
        }
    }

    
}
