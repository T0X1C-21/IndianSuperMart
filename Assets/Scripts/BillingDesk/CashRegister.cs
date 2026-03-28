using UnityEngine;

public class CashRegister : MonoBehaviour {


    private const string OPEN = "open";
    private const string CLOSE = "close";


    [SerializeField] private BillingDesk billingDesk;
    

    private Animator animator;
    private bool isOpen;


    private void Awake() {
        animator = this.GetComponent<Animator>();
    }

    private void Start() {
        billingDesk.OnStartPaymentMode += BillingDesk_OnStartPaymentMode;
        billingDesk.OnEndPaymentMode += BillingDesk_OnEndPaymentMode;
    }

    private void BillingDesk_OnEndPaymentMode(object sender, System.EventArgs e) {
        if (isOpen) {
            AnimateCashShelfClose();
            isOpen = false;
        } 
    }

    private void BillingDesk_OnStartPaymentMode(object sender, System.EventArgs e) {
        if(!isOpen) {
            AnimateCashShelfOpen();
            isOpen = true;
        }
    }

    private void AnimateCashShelfOpen() {
        animator.SetTrigger(OPEN);
    }

    private void AnimateCashShelfClose() {
        animator.SetTrigger(CLOSE);
    }


}
