using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPriceMenuUI : MonoBehaviour {


    public static SetPriceMenuUI Instance { get; private set; }


    public event EventHandler<OnPriceChangeForItemSOEventArgs> OnPriceChangedForItemSO;
    public class OnPriceChangeForItemSOEventArgs : EventArgs {
        public ItemSO itemSO;
    }


    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject priceMenuUIGameObject;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAveragePriceTMP;
    [SerializeField] private TextMeshProUGUI itemCurrentPriceTMP;
    [SerializeField] private TextMeshProUGUI itemProfitPriceTMP;
    [SerializeField] private Button okButton;


    private ItemSO itemSO;


    private void Awake() {
        Instance = this;
        priceMenuUIGameObject.SetActive(false);

        inputField.onValidateInput += InvalidateNegativeNumbers;
        inputField.onEndEdit.AddListener(GetPriceFromInputField);

        okButton.onClick.AddListener(OnOkButtonClick);
    }

    private void OnOkButtonClick() {
        DisableUI();
    }

    // To prevent from writing negative digits with '-'
    private char InvalidateNegativeNumbers(string text, int charIndex, char addedChar) {
        return char.IsDigit(addedChar) ? addedChar : '\0';
    }

    private void GetPriceFromInputField(string numberString) {
        int priceNumber = int.Parse(numberString);
        itemSO.currentPrice = priceNumber;

        SetUIElements();

        // Change price amount in price tag UI
        OnPriceChangedForItemSO?.Invoke(this, new OnPriceChangeForItemSOEventArgs {
            itemSO = this.itemSO
        });
    }

    private void Update() {
        bool isCancelPressedThisFrame = GameInput.Instance.GetCancelBool();

        // Escape to close the UI
        if (isCancelPressedThisFrame) {
            DisableUI();
        }
    }

    private void DisableUI() {
        priceMenuUIGameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameInput.Instance.SetCanGiveInput(true);
    }

    public void EnableUI(ItemSO itemSO) {
        this.itemSO = itemSO;
        SetUIElements();

        priceMenuUIGameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        GameInput.Instance.SetCanGiveInput(false);
    }

    private void SetUIElements() {
        itemImage.sprite = itemSO.itemSprite;
        itemAveragePriceTMP.text = "Average: ₹" + itemSO.averagePrice;
        itemCurrentPriceTMP.text = "Current: ₹" + itemSO.currentPrice;
        itemProfitPriceTMP.text = "Profit: ₹" + (itemSO.currentPrice - itemSO.averagePrice);

        inputField.text = "";
    }


}
