using UnityEngine;

public class CashRegister : MonoBehaviour {


    private const string OPEN = "open";
    private const string CLOSE = "close";


    [SerializeField] private BillingDeskInteractable billingDeskInteractable;
    

    private Animator animator;


    private void Awake() {
        animator = this.GetComponent<Animator>();
    }

    private void Start() {
        billingDeskInteractable.OnBillingDeskEquipped += BillingDeskInteractable_OnBillingDeskEquipped;
        billingDeskInteractable.OnBillingDeskUnequipped += BillingDeskInteractable_OnBillingDeskUnequipped;
    }

    private void BillingDeskInteractable_OnBillingDeskUnequipped(object sender, System.EventArgs e) {
        AnimateCashShelfClose();
    }

    private void BillingDeskInteractable_OnBillingDeskEquipped(object sender, System.EventArgs e) {
        AnimateCashShelfOpen();
    }

    private void AnimateCashShelfOpen() {
        animator.SetTrigger(OPEN);
    }

    private void AnimateCashShelfClose() {
        animator.SetTrigger(CLOSE);
    }

}
