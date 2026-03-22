using System;
using System.Collections;
using UnityEngine;

public interface IInteractableObject { 
    

    public event EventHandler OnPickedUp;
    public event EventHandler OnPlacedInWorld;


    public abstract void Interact(Player player);
    public abstract void UnparentAndSetToPosition(Vector3 worldPosition, Player player);
    public Collider GetCollider();
    public MeshRenderer GetMeshRenderer();
    public IEnumerator AnimateInteractableObjectToPlayerHand(Vector3 playerHandPosition);
    public IEnumerator AnimateInteractableObjectToWorld(Vector3 worldPosition, Quaternion targetRotation);


}
