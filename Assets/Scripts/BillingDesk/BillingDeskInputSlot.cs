using UnityEngine;

public class BillingDeskInputSlot : MonoBehaviour, IInteractableObject {


    [SerializeField] private Transform targetTransform;
    [SerializeField] private new BoxCollider collider;
    [SerializeField] private Item containingItem;
    

    private bool isContainingItem = true;


    public void SendItemToOutputSlot() {
        containingItem.transform.parent = targetTransform;
        StartCoroutine(containingItem.AnimateItemToSlot(targetTransform.position, Quaternion.Euler(0f, -90f, 0f)));
        collider.enabled = false;
    }

    public void Interact() {
        if (isContainingItem) {
            SendItemToOutputSlot();
        }
    }
    

}
