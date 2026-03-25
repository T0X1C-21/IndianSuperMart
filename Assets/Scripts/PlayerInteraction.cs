using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {


    [Header("Interaction Settings")]
    [SerializeField] private Transform originTransform;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask shelfLayerMask;
    [SerializeField] private LayerMask interactableUILayerMask;
    [SerializeField] private float interactDistance;


    private Player player;


    private void Awake() {
        player = this.GetComponent<Player>();
    }

    private void Update() {
        Interact();
    }

    private void Interact() {
        bool interactPressedThisFrame = GameInput.Instance.GetInteractBool();

        if (!interactPressedThisFrame) {
            return;
        }

        RaycastHit hitInfo;

        if(RaycastAndCheckForLayer(interactableLayerMask, out hitInfo) && !player.GetIsCarryingSomething()) {
            // Player interacted with an interactable object

            Debug.Log(hitInfo.collider);

            IInteractableObject interactableObject = hitInfo.transform.GetComponent<IInteractableObject>();
            interactableObject.Interact();

        } else if(RaycastAndCheckForLayer(pickableLayerMask, out hitInfo) && !player.GetIsCarryingSomething()) {
            // Player interacted with an pickable object
            
            IPickableObject pickableObject = hitInfo.transform.GetComponent<IPickableObject>();
            pickableObject?.Interact(player);

        } else if (RaycastAndCheckForLayer(interactableUILayerMask, out hitInfo)) {
            // Player interacted with world space interactable UI

            InteractableUI interactableUI = hitInfo.transform.GetComponent<InteractableUI>();
            interactableUI?.OnInteract();


        } else if (RaycastAndCheckForLayer(shelfLayerMask, out hitInfo) && player.GetIsCarryingSomething()) {
            // Player interacted with shelf

            IPickableObject carryingInteractableObject = player.GetCarryingInteractableObject();
            ShelfSlot shelfSlot = hitInfo.transform.GetComponent<ShelfSlot>();
            
            if(shelfSlot == null) {
                Debug.LogWarning("PlayerInteraction | ShelfSlot not found!");
            }

            if(carryingInteractableObject is ItemContainer) {
                ItemContainer itemContainer = carryingInteractableObject as ItemContainer;
                if (!itemContainer.IsEmpty() && !shelfSlot.IsFull()) {
                    shelfSlot.AttachObjectToFreeSlot(itemContainer.GetNextItem());
                }
            }
        } else if(RaycastAndCheckForLayer(groundLayerMask, out hitInfo) && player.GetIsCarryingSomething()) {
            // Player interacted with ground
            if (player.GetIsCarryingSomething()) {
                // Player is carrying something
                IPickableObject carryingInteractableObject = player.GetCarryingInteractableObject();

                if (carryingInteractableObject == null) {
                    Debug.LogWarning("PlayerInteraction | CarryingInteractableObject not found!");
                }

                // Check if the picked up object is overlapping with other pickable objects
                Collider collider = carryingInteractableObject.GetCollider();
                Bounds bounds = collider.bounds;
                Vector3 halfExtents = bounds.extents;
                Vector3 placingPosition = hitInfo.point;
                halfExtents = Vector3.Max(halfExtents, Vector3.one * 0.1f);

                Collider[] overlappingColliderArray = Physics.OverlapBox(placingPosition, halfExtents,
                    collider.transform.rotation, pickableLayerMask);

                foreach (Collider overlappingCollider in overlappingColliderArray) {
                    if (overlappingCollider != collider) {
                        // Overlapped with other pickable object
                        return;
                    }
                }

                carryingInteractableObject.UnparentAndSetToPosition(placingPosition, player);
            }
        }
    }

    private bool RaycastAndCheckForLayer(LayerMask layerMask, out RaycastHit hitInfo) {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 
            interactDistance, layerMask);
    }


}
