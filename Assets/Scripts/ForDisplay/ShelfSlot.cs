using System;
using UnityEngine;

public class ShelfSlot : MonoBehaviour {


    public event EventHandler<OnNewItemAttachedEventArgs> OnNewItemAttached;
    public class OnNewItemAttachedEventArgs : EventArgs {
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

            OnNewItemAttached?.Invoke(this, new OnNewItemAttachedEventArgs {
                itemSO = newItemSO
            });
        }

        if (IsFull()) {
            return;
        }

        if(itemSO.itemType == item.GetItemSO().itemType) {
            Transform parentTransform = slotTransformArray[freeSlotTransformIndex++].transform;

            item.transform.parent = parentTransform;
            StartCoroutine(item.AnimateItemToSlot(parentTransform.transform.position, Quaternion.identity));
        }
    }

    private bool IsEmpty() {
        return freeSlotTransformIndex == 0;
    }

    public bool IsFull() {
        return freeSlotTransformIndex == slotTransformArray.Length;
    }

    
}
