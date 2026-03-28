using UnityEngine;

public class BillingDeskOutputSlot : MonoBehaviour {
    

    private bool isContainingItem;


    public void SetIsContainingItem(bool value) {
        isContainingItem = value;
    }

    public bool GetIsContainingItem() {
        return isContainingItem;
    }

    public void RemoveItem() {
        Destroy(this.transform.GetChild(0).gameObject);
        SetIsContainingItem(false);
    }


}
