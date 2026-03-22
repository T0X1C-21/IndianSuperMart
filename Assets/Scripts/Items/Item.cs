using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {


    public enum ItemType {
        
        Belloggs

    }


    [SerializeField] protected ItemSO itemSO;


    private Vector3 defaultPosition = Vector3.zero;
    private Quaternion defaultRotation = Quaternion.identity;


    private void Awake() {
        // Reset current price to average price
        itemSO.currentPrice = itemSO.averagePrice;
    }

    public IEnumerator AnimateItemToSlot(Vector3 slotPosition) {
        Vector3 itemStartPosition = this.transform.position;
        Quaternion itemStartRotation = this.transform.rotation;
        float animationSpeed = 5f;
        float t = 0f;
        while(t <= 1f) {
            t += Time.deltaTime * animationSpeed;
            this.transform.position = Vector3.Lerp(itemStartPosition, slotPosition, t);
            this.transform.localRotation = Quaternion.Slerp(itemStartRotation, defaultRotation, t);
            yield return null;
        }
        this.transform.SetLocalPositionAndRotation(defaultPosition, defaultRotation);
    }

    public ItemSO GetItemSO() {
        return itemSO;
    }

    
}
