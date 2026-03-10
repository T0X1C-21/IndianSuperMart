using UnityEngine;

public class Player : MonoBehaviour {


    public static Player Instance { get; private set; }


    [Header("Player Settings")]
    [SerializeField] private Transform originTransform;

    [Header("Object Pickup Settings")]
    [SerializeField] private Transform objectPickupPointTransform;
    [SerializeField] private Vector3 objectPickupPointPositionOffset;
    [SerializeField] private float objectPickupPointDistance;


    private bool isCarryingSomething;
    private IInteractableObject carryingInteractableObject;


    private void Awake() {
        Instance = this;
    }

    public Transform GetOriginTransform() {
        return originTransform;
    }

    public Transform GetObjectPickupPoint() {
        return objectPickupPointTransform;
    }

    public bool GetIsCarryingSomething() {
        return isCarryingSomething; 
    }

    public void SetIsCarryingSomething(bool value) {
        isCarryingSomething = value;
    }

    public IInteractableObject GetCarryingInteractableObject() {
        return carryingInteractableObject;
    }

    public void SetCarryingInteractableObject(IInteractableObject interactableObject) {
        carryingInteractableObject = interactableObject;
    }


}
