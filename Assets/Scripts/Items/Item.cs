using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {


    public enum ItemType {
        
        Belloggs

    }


    [SerializeField] protected ItemSO itemSO;
    [SerializeField] private AnimationCurve verticalAnimationCurve;


    private Vector3 defaultPosition = Vector3.zero;


    private void Awake() {
        // Reset current price to average price
        itemSO.currentPrice = itemSO.averagePrice;
    }

    public IEnumerator AnimateItemToSlot(Vector3 slotPosition, Quaternion targetRotation) {
        Vector3 itemStartPosition = this.transform.position;
        Quaternion itemStartRotation = this.transform.rotation;
        float animationSpeed = 3f;
        float t = 0f;
        while(t <= 1f) {
            t += Time.deltaTime * animationSpeed;
            Vector3 linearLerpedPosition = Vector3.Lerp(itemStartPosition, slotPosition, t);
            Vector3 extraVerticalPosition = new Vector3(0f, verticalAnimationCurve.Evaluate(t), 0f);
            this.transform.position = linearLerpedPosition + extraVerticalPosition;
            this.transform.localRotation = Quaternion.Slerp(itemStartRotation, targetRotation, t);
            yield return null;
        }
        this.transform.SetLocalPositionAndRotation(defaultPosition, targetRotation);
    }

    public ItemSO GetItemSO() {
        return itemSO;
    }

    
}
