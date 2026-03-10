using UnityEngine;

public class Item : MonoBehaviour {


    public enum ItemType {
        
        Belloggs

    }


    [SerializeField] protected ItemSO itemSO;


    private void Awake() {
        // Reset current price to average price
        itemSO.currentPrice = itemSO.averagePrice;
    }

    public ItemSO GetItemSO() {
        return itemSO;
    }

    
}
