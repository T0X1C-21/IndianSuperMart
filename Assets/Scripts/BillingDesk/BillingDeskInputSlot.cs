using UnityEngine;

public class BillingDeskInputSlot : MonoBehaviour, IInteractableObject {


    [SerializeField] BillingDeskItemSlotsManager billingDeskItemSlotsManager;
    [SerializeField] private new BoxCollider collider;
    

    private Item containingItem;
    private bool isContainingItem;
    private bool canInteract;


    public void AddItemToSelf(Item item) {
        if (isContainingItem) {
            Debug.LogError($"{this.transform.name} already contains an Item!");
        }

        item.transform.parent = this.transform;
        StartCoroutine(item.AnimateItemToSlot(this.transform.position, Quaternion.identity));
        containingItem = item;
        isContainingItem = true;
    }

    public void SendItemToOutputSlot() {
        if(containingItem == null) {
            Debug.LogError($"There's no containing Item for {this.transform.name}!");
        }

        BillingDeskOutputSlot freeOutputSlot = billingDeskItemSlotsManager.GetNextFreeOutputSlot();

        if(freeOutputSlot == null) {
            Debug.LogError($"freeOutputSlotTransform is null");
            return;
        }

        containingItem.transform.parent = freeOutputSlot.transform;
        StartCoroutine(containingItem.AnimateItemToSlot(freeOutputSlot.transform.position, Quaternion.identity));
        collider.enabled = false;

        billingDeskItemSlotsManager.DecreaseRemainingItemsToScan();
    }

    public void Interact() {
        if (isContainingItem && canInteract) {
            SendItemToOutputSlot();
        }
    }

    public void SetCanInteract(bool value) {
        canInteract = value;
    }
    

}
