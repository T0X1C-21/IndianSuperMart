using System;
using System.Collections;
using UnityEngine;

public class ItemContainer : MonoBehaviour, IPickableObject {


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
        Transform playerPickupPoint = player.GetObjectPickupPoint();
        this.transform.parent = playerPickupPoint;
        StartCoroutine(AnimateInteractableObjectToPlayerHand(playerPickupPoint.transform.position));

        player.SetIsCarryingSomething(true);
        player.SetCarryingInteractableObject(this);
        this.collider.enabled = false;

        OnPickedUp?.Invoke(this, EventArgs.Empty);
    }

    public void UnparentAndSetToPosition(Vector3 worldPosition, Player player) {
        player.SetIsCarryingSomething(false);
        player.SetCarryingInteractableObject(null);
        
        this.transform.parent = null;
        Quaternion targetRotationRaw = Quaternion.LookRotation(-player.transform.forward, Vector3.up);
        Quaternion targetRotation = Quaternion.Euler(targetRotationRaw.eulerAngles + new Vector3(0f, 90f, 0f));
        StartCoroutine(AnimateInteractableObjectToWorld(worldPosition, targetRotation));
        this.collider.enabled = true;

        OnPlacedInWorld?.Invoke(this, EventArgs.Empty);
    }

    public IEnumerator AnimateInteractableObjectToPlayerHand(Vector3 playerHandPosition) {
        Vector3 interactableStartPosition = this.transform.position;
        Quaternion interactableStartRotation = this.transform.rotation;
        float animationSpeed = 5f;
        float t = 0f;
        while(t <= 1f) {
            t += Time.deltaTime * animationSpeed;
            Vector3 lerpedPosition = Vector3.Lerp(interactableStartPosition, playerHandPosition, t);
            Quaternion lerpedRotation = Quaternion.Slerp(interactableStartRotation, defaultRotation, t);
            this.transform.position = lerpedPosition;
            this.transform.localRotation = lerpedRotation;
            yield return null;
        }
        this.transform.SetLocalPositionAndRotation(defaultPosition, defaultRotation);
    }

    public IEnumerator AnimateInteractableObjectToWorld(Vector3 worldPosition, Quaternion targetRotation) {
        Vector3 interactableStartPosition = this.transform.position;
        Quaternion interactableStartRotation = this.transform.rotation;
        float animationSpeed = 5f;
        float t = 0f;
        while(t <= 1f) {
            t += Time.deltaTime * animationSpeed;
            Vector3 lerpedPosition = Vector3.Lerp(interactableStartPosition, worldPosition, t);
            Quaternion lerpedRotation = Quaternion.Slerp(interactableStartRotation, targetRotation, t);
            this.transform.position = lerpedPosition;
            this.transform.localRotation = lerpedRotation;
            yield return null;
        }
        this.transform.position = worldPosition;
        this.transform.localRotation = targetRotation;
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
