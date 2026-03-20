using System;
using UnityEngine;

public class ItemContainer : MonoBehaviour, IInteractableObject {


    public event EventHandler OnPickedUp;
    public event EventHandler OnPlacedInWorld;


    [SerializeField] private Item.ItemType itemType;
    [SerializeField] private Item[] itemArray;
    [SerializeField] private new Collider collider;
    [SerializeField] private MeshRenderer meshRenderer;


    private int currentItemIndex;
    private Vector3 defaultPosition = Vector3.zero;
    private Quaternion defaultRotation = Quaternion.Euler(0f, 180f, 0f);


    private void Awake() {
        currentItemIndex = itemArray.Length - 1;
    }

    public Item.ItemType GetItemType() {
        return itemType;
    }

    public Item GetNextItem() {
        if(currentItemIndex < 0) {
            return null;
        }
            return itemArray[currentItemIndex--];
    }

    public void Interact(Player player) {
        this.transform.parent = player.GetObjectPickupPoint();
        this.transform.SetLocalPositionAndRotation(defaultPosition, defaultRotation);

        player.SetIsCarryingSomething(true);
        player.SetCarryingInteractableObject(this);
        this.collider.enabled = false;

        OnPickedUp?.Invoke(this, EventArgs.Empty);
    }

    public void UnparentAndSetToPosition(Vector3 worldPosition, Player player) {
        player.SetIsCarryingSomething(false);
        player.SetCarryingInteractableObject(null);
        
        this.transform.parent = null;
        this.transform.position = worldPosition;
        this.collider.enabled = true;

        //Vector3 directionToPlayer = player.transform.position - this.transform.position;
        //this.transform.forward = directionToPlayer.normalized;
        this.transform.forward = -player.transform.forward;

        OnPlacedInWorld?.Invoke(this, EventArgs.Empty);
    }

    public Collider GetCollider() {
        return collider;
    }

    public MeshRenderer GetMeshRenderer() {
        return meshRenderer;
    }

    public bool IsEmpty() {
        return currentItemIndex == -1;
    }

    
}
