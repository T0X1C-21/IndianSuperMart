using UnityEngine;

public class CurrencyCollider : MonoBehaviour, IInteractableObject {


    [SerializeField] private BillingDesk billingDesk;
    [SerializeField] private CurrencySO currencySO;
    [SerializeField] private CurrencySlotsManager currencySlotsManager;


    private bool canInteract;


    private void Start() {
        billingDesk.OnStartPaymentMode += BillingDesk_OnStartPaymentMode;
        billingDesk.OnEndPaymentMode += BillingDesk_OnEndPaymentMode;
    }

    private void BillingDesk_OnEndPaymentMode(object sender, System.EventArgs e) {
        canInteract = false;
    }

    private void BillingDesk_OnStartPaymentMode(object sender, System.EventArgs e) {
        canInteract = true;
    }

    public void Interact() {
        if (!canInteract) {
            return;
        }

        GameObject currencyPrefab = currencySO.gameObject;
        GameObject spawnedCurrency = Instantiate(currencyPrefab, this.transform.position, this.transform.rotation);

        Currency currency = spawnedCurrency.GetComponent<Currency>();
        Vector3 targetPosition = currencySlotsManager.AddCurrencyAndGetPosition(currency, out Transform parentTransform);
        spawnedCurrency.transform.parent = parentTransform;
        StartCoroutine(currency.AnimateCurrencyToSlot(targetPosition, Quaternion.identity));
    }


}
