using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputActions _controls;
    private Vector2 movementInputDirection = Vector2.zero;
    private Vector2 lookInputDirection = Vector2.zero;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new InputActions();
            _controls.Enable();
            _controls.Player.Movement.performed += ctx => movementInputDirection = ctx.ReadValue<Vector2>();
            _controls.Player.Look.performed += ctx => lookInputDirection = ctx.ReadValue<Vector2>();
        }
    }

    public Vector2 MovementInputDirection
    {
        get => movementInputDirection;
        set => movementInputDirection = value;
    }

    public Vector2 LookInputDirection
    {
        get => lookInputDirection;
        set => lookInputDirection = value;
    }


    private void OnDisable()
    {
        _controls.Disable();
    }
}
