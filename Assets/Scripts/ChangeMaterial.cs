using UnityEngine;

public class ChangeMaterial : MonoBehaviour {


    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;


    private IInteractableObject interactableObject;


    private void InteractableObject_OnPlacedInWorld(object sender, System.EventArgs e) {
        SetNormalMaterials();
    }

    private void InteractableObject_OnPickedUp(object sender, System.EventArgs e) {
        SetSelectedMaterials();
    }

    private void SetNormalMaterials() {
        Material[] materials = { normalMaterial };
        meshRenderer.materials = materials;
    }

    private void SetSelectedMaterials() {
        Material[] materials = { normalMaterial, selectedMaterial };
        meshRenderer.materials = materials;
    }

    private void OnTransformParentChanged() {
        // Unsubscribe to old parent events
        if (interactableObject != null) {
            interactableObject.OnPickedUp -= InteractableObject_OnPickedUp;
            interactableObject.OnPlacedInWorld -= InteractableObject_OnPlacedInWorld;

            interactableObject = null;
        }

        // Loop parent after parent to get IInteractableObject
        Transform transform = this.transform;
        do {
            interactableObject = transform.GetComponent<IInteractableObject>();

            if(interactableObject != null) {
                break;
            }

            transform = transform.parent;
        } while(transform != null); // There's a parent to "transform"

        // Subscribe to new parent events else reset to normal materials
        if (interactableObject != null) {
            interactableObject.OnPickedUp += InteractableObject_OnPickedUp;
            interactableObject.OnPlacedInWorld += InteractableObject_OnPlacedInWorld;
        } else {
            SetNormalMaterials();
        }
    }


}
