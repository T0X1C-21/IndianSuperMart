using UnityEngine;

public class ItemContainerVisual : MonoBehaviour {


    private const string OPEN = "open";
    private const string CLOSE = "close";


    [SerializeField] private ItemContainer itemContainer;


    private Animator animator;


    private void Awake() {
        animator = this.GetComponent<Animator>();
    }

    private void Start() {
        itemContainer.OnPickedUp += ItemContainer_OnPickedUp;
        itemContainer.OnPlacedInWorld += ItemContainer_OnPlacedInWorld;
    }

    private void ItemContainer_OnPlacedInWorld(object sender, System.EventArgs e) {
        ItemContainerClosingAnimation();
    }

    private void ItemContainer_OnPickedUp(object sender, System.EventArgs e) {
        ItemContainerOpeningAnimation();
    }

    private void ItemContainerOpeningAnimation() {
        animator.SetTrigger(OPEN);
    }

    private void ItemContainerClosingAnimation() {
        animator.SetTrigger(CLOSE);
    }


}
