using TMPro;
using UnityEngine;

public class ProductSummaryTemplate : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI numberOfUnitsTMP;
    [SerializeField] private TextMeshProUGUI currentPriceTMP;
    [SerializeField] private TextMeshProUGUI totalPriceTMP;


    public void SetParametersAndReturnTotal(ItemSO itemSO, int numberOfUnits, int currentTotal, out int totalPrice) {
        itemNameTMP.text = itemSO.name;
        numberOfUnitsTMP.text = numberOfUnits.ToString();
        currentPriceTMP.text = "₹" + itemSO.currentPrice.ToString();

        int itemTotal = numberOfUnits * itemSO.currentPrice;
        totalPriceTMP.text = "₹" + itemTotal.ToString();

        totalPrice = currentTotal;
        totalPrice += itemTotal;
    }

    
}
