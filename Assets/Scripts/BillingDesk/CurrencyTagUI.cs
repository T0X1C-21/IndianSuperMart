using UnityEngine;

public class CurrencyTagUI : MonoBehaviour, IHoverable {


    [SerializeField] private GameObject currencyTagUIGameObject;
    [SerializeField] private BillingDesk billingDesk;


    private bool isHovered;
    private bool canHover;


    private void Start() {
        billingDesk.OnStartPaymentMode += BillingDesk_OnStartPaymentMode;
        billingDesk.OnEndPaymentMode += BillingDesk_OnEndPaymentMode;
    }

    private void BillingDesk_OnEndPaymentMode(object sender, System.EventArgs e) {
        canHover = false;
    }

    private void BillingDesk_OnStartPaymentMode(object sender, System.EventArgs e) {
        canHover = true;
    }

    private void Update() {
        isHovered = false;
        HideUI();
    }

    private void LateUpdate() {
        if (isHovered && canHover) {
            ShowUI();
        }
    }

    private void ShowUI() {
        currencyTagUIGameObject.SetActive(true);
    }

    private void HideUI() {
        currencyTagUIGameObject.SetActive(false);
    }

    public void OnHover() {
        isHovered = true;
    }


}
