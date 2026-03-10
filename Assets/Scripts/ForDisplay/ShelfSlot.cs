using System;
using UnityEngine;

public class ShelfSlot : MonoBehaviour {


    public event EventHandler<OnItemAttachedEventArgs> OnItemAttached;
    public class OnItemAttachedEventArgs : EventArgs {
        public ItemSO itemSO;
    }

    // To be added
    //public event EventHandler OnAllItemsRemoved;


    [SerializeField] private Transform[] slotTransformArray;


    private int freeSlotTransformIndex;
    private ItemSO itemSO;


    public void AttachObjectToFreeSlot(Item item) {
        if (IsEmpty()) {
            ItemSO newItemSO = item.GetItemSO();

            itemSO = newItemSO;

            OnItemAttached?.Invoke(this, new OnItemAttachedEventArgs {
                itemSO = newItemSO
            });
        }

        if (IsFull()) {
            return;
        }

        if(itemSO.itemType == item.GetItemSO().itemType) {
            Transform parentTransform = slotTransformArray[freeSlotTransformIndex++].transform;

            item.transform.parent = parentTransform;
            item.transform.localPosition = Vector3.zero;
            item.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
    }

    private bool IsEmpty() {
        return freeSlotTransformIndex == 0;
    }

    public bool IsFull() {
        return freeSlotTransformIndex == slotTransformArray.Length;
    }

    
}
