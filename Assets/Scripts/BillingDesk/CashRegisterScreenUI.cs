using TMPro;
using UnityEngine;

public class CashRegisterScreenUI : MonoBehaviour {


    [SerializeField] private GameObject screenUIGameObject;
    [SerializeField] private BillingDesk billingDesk;
    [SerializeField] private BillingDeskInputSlot[] billingDeskInputSlotArray;
    [SerializeField] private CurrencySlotsManager currencySlotsManager;
    [SerializeField] private CurrencySlot[] currencySlotArray;
    [SerializeField] private TextMeshProUGUI receivedValueTMP;
    [SerializeField] private TextMeshProUGUI totalValueTMP;
    [SerializeField] private TextMeshProUGUI changeValueTMP;
    [SerializeField] private TextMeshProUGUI givingValueTMP;


    private int currentReceivedValue;
    private int currentTotalValue;
    private int currentGivingValue;


    private void Awake() {
        DisableUI();
    }

    private void Start() {
        billingDesk.OnStartScanMode += BillingDesk_OnStartScanMode;
        billingDesk.OnEndPaymentMode += BillingDesk_OnEndPaymentMode;
        currencySlotsManager.OnGivingAmountAdded += CurrencySlotsManager_OnChangeAddedAmount;
        foreach(CurrencySlot currencySlot in currencySlotArray) {
            currencySlot.OnGivingAmountRemoved += CurrencySlot_OnGivingAmountRemoved;
        }
        foreach(BillingDeskInputSlot billingDeskInputSlot in billingDeskInputSlotArray) {
            billingDeskInputSlot.OnItemScanned += BillingDeskInputSlot_OnItemScanned;
        }
    }

    private void BillingDeskInputSlot_OnItemScanned(object sender, BillingDeskInputSlot.OnItemScannedEventArgs e) {
        currentTotalValue += e.itemPriceAmount;
        UpdateTotalValue(currentTotalValue);
        UpdateChangeValue(currentReceivedValue - currentTotalValue);
    }

    private void BillingDesk_OnStartScanMode(object sender, BillingDesk.OnStartScanModeEventArgs e) {
        currentReceivedValue = e.receivedAmount;
        UpdateReceivedValue(currentReceivedValue);
        EnableUI();
    }

    private void CurrencySlot_OnGivingAmountRemoved(object sender, CurrencySlot.OnGivingAmountRemovedEventArgs e) {
        currentGivingValue -= e.givingAmountRemoved;
        UpdateGivingValue(currentGivingValue);
    }

    private void CurrencySlotsManager_OnChangeAddedAmount(object sender, CurrencySlotsManager.OnGivingAmountAddedEventArgs e) {
        currentGivingValue += e.givingAmountAdded;
        UpdateGivingValue(currentGivingValue);
    }

    private void BillingDesk_OnEndPaymentMode(object sender, System.EventArgs e) {
        DisableUI();
    }

    private void EnableUI() {
        screenUIGameObject.SetActive(true);
    }

    private void DisableUI() {
        screenUIGameObject.SetActive(false);
    }

    private void UpdateReceivedValue(int receivedAmount) {
        receivedValueTMP.text = "₹" + receivedAmount.ToString();
    }

    private void UpdateTotalValue(int totalAmount) {
        totalValueTMP.text = "₹" + totalAmount.ToString();
    }

    private void UpdateChangeValue(int changeAmount) {
        changeValueTMP.text = "₹" + changeAmount.ToString();
    }

    private void UpdateGivingValue(int givingAmount) {
        givingValueTMP.text = "₹" + givingAmount.ToString();
    }

    
}
