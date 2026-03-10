using System;
using UnityEngine;

public class GameInput : MonoBehaviour {


    public static GameInput Instance { get; private set; }


    private InputActions inputActions;
    private bool canGiveInput;


    private void Awake() {
        Instance = this;

        inputActions = new InputActions();
        canGiveInput = true;
    }

    private void OnEnable() {
        inputActions.Player.Run.Enable();
        inputActions.Player.Look.Enable();
        inputActions.Player.Interact.Enable();
        inputActions.Player.Cancel.Enable();
    }

    private void OnDisable() {
        inputActions.Player.Run.Disable();
        inputActions.Player.Look.Disable();
        inputActions.Player.Interact.Disable();
        inputActions.Player.Cancel.Disable();
    }

    public Vector2 GetRunInputVector() {
        if (!canGiveInput) {
            return Vector2.zero;
        }
        return inputActions.Player.Run.ReadValue<Vector2>();
    }

    public Vector2 GetLookInputVector() {
        if (!canGiveInput) {
            return Vector2.zero;
        }
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool GetInteractBool() {
        return inputActions.Player.Interact.triggered;
    }

    public bool GetCancelBool() {
        return inputActions.Player.Cancel.triggered;
    } 

    public void SetCanGiveInput(bool value) {
        canGiveInput = value;
    }


}
