using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputActions _controls;
    private Vector2 _movementInputDirection = Vector2.zero;
    private Vector2 _lookInputDirection = Vector2.zero;
    private float _actionInput = 0;
    private Vector2 _navigationInputDirection = Vector2.zero;
    private float _confirmButton = 0;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new InputActions();
            _controls.Enable();
            _controls.Player.Movement.performed += ctx => _movementInputDirection = ctx.ReadValue<Vector2>();
            _controls.Player.Movement.canceled += ctx => _movementInputDirection = Vector2.zero;
            _controls.Player.Look.performed += ctx => _lookInputDirection = ctx.ReadValue<Vector2>();
            _controls.Player.Action.performed += ctx => _actionInput = ctx.ReadValue<float>();
            _controls.Player.Action.canceled += ctx => _actionInput = 0;
            _controls.Menu.Navigate.performed += ctx => _navigationInputDirection = ctx.ReadValue<Vector2>();
            _controls.Menu.Navigate.canceled += ctx => _navigationInputDirection = Vector2.zero;
            _controls.Menu.Confirm.performed += ctx => _confirmButton = ctx.ReadValue<float>();
            _controls.Menu.Confirm.canceled += ctx => _confirmButton = 0;
        }
    }

    public Vector2 MovementInputDirection => _movementInputDirection;

    public Vector2 LookInputDirection => _lookInputDirection;

    public float ActionInput => _actionInput;

    public Vector2 NavigationInputDirection => _navigationInputDirection;

    public float ConfirmButton => _confirmButton;

    private void OnDisable()
    {
        _controls.Disable();
    }
}
