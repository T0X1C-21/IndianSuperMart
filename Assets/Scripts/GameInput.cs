using System;
using UnityEngine;

public class GameInput : MonoBehaviour {


    public static GameInput Instance { get; private set; }


    private InputActions inputActions;


    private void Awake() {
        Instance = this;

        inputActions = new InputActions();

        inputActions.Player.Run.Enable();
        inputActions.Player.Look.Enable();
    }

    public Vector2 GetRunInputVector() {
        return inputActions.Player.Run.ReadValue<Vector2>();
    }

    public Vector2 GetLookInputVector() {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }


}
