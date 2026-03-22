using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceTag : MonoBehaviour, InteractableUI {


    [SerializeField] private ShelfSlot attachedShelfSlot;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPriceTagTMP;


    private ItemSO currentItemSO;


    private void Awake() {
        DisableItemImageAndPriceTag();
    }

    private void Start() {
        attachedShelfSlot.OnNewItemAttached += AttachedShelfSlot_OnItemAttached;
        //attachedShelfSlot.OnAllItemsRemoved += AttachedShelfSlot_OnAllItemsRemoved;

        SetPriceMenuUI.Instance.OnPriceChangedForItemSO += SetPriceMenuUI_OnPriceChangedForItemSO;
    }

    private void SetPriceMenuUI_OnPriceChangedForItemSO(object sender, SetPriceMenuUI.OnPriceChangeForItemSOEventArgs e) {
        if(e.itemSO.itemType == currentItemSO?.itemType) {
            ChangePriceTag(e.itemSO.currentPrice);
        }
    }

    //private void AttachedShelfSlot_OnAllItemsRemoved(object sender, System.EventArgs e) {
    //    DisableItemImageAndPriceTag();
    //}

    private void AttachedShelfSlot_OnItemAttached(object sender, ShelfSlot.OnNewItemAttachedEventArgs e) {
        SetItemImageAndPriceTag(e.itemSO);
    }

    private void SetItemImageAndPriceTag(ItemSO itemSO) {
        itemImage.sprite = itemSO.itemSprite;
        itemPriceTagTMP.text = "₹" + itemSO.currentPrice;

        itemImage.gameObject.SetActive(true);
        itemPriceTagTMP.gameObject.SetActive(true);

        currentItemSO = itemSO;
    }

    private void DisableItemImageAndPriceTag() {
        itemImage.gameObject.SetActive(false);
        itemPriceTagTMP.gameObject.SetActive(false);

        currentItemSO = null;
    }

    public void OnInteract() {
        // There's no item on this shelf
        if(currentItemSO == null) {
            return;
        }

        UIManager.Instance.OnPriceTagInteract(currentItemSO);
    }

    private void ChangePriceTag(int priceAmount) {
        itemPriceTagTMP.text = "₹" + Mathf.Abs(priceAmount);
    }


}
