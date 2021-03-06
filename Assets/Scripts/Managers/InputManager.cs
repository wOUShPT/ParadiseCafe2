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
    private float _escapeButton = 0;
    private float _quitButton = 0;
    private Vector2 _freeCamHorizontalMovementInputDirection = Vector2.zero;
    private float _freeCamVerticalMovementInputDirection = 0;
    private Vector2 _freeCamLookInputDirection = Vector2.zero;
    

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
            _controls.Player.Escape.performed += ctx => _escapeButton = ctx.ReadValue<float>();
            _controls.Player.Escape.canceled += ctx => _escapeButton = 0;
            _controls.Player.Quit.performed += ctx => _quitButton = ctx.ReadValue<float>();
            _controls.Player.Quit.canceled += ctx => _quitButton = 0;
            _controls.FreeCam.HorizontalMovement.performed += ctx => _freeCamHorizontalMovementInputDirection = ctx.ReadValue<Vector2>();
            _controls.FreeCam.HorizontalMovement.canceled += ctx => _freeCamHorizontalMovementInputDirection = Vector2.zero;
            _controls.FreeCam.VerticalMovement.performed += ctx => _freeCamVerticalMovementInputDirection = ctx.ReadValue<float>();
            _controls.FreeCam.VerticalMovement.canceled += ctx => _freeCamVerticalMovementInputDirection = 0;
            _controls.FreeCam.Look.performed += ctx => _freeCamLookInputDirection = ctx.ReadValue<Vector2>();
            _controls.FreeCam.Look.canceled += ctx => _freeCamLookInputDirection = Vector2.zero;
        }
    }

    public Vector2 MovementInputDirection => _movementInputDirection;

    public Vector2 LookInputDirection => _lookInputDirection;

    public float ActionInput => _actionInput;

    public Vector2 NavigationInputDirection => _navigationInputDirection;

    public float ConfirmButton => _confirmButton;

    public float EscapeButton => _escapeButton;

    public float QuitButton => _quitButton;
    
    public Vector3 FreeCamMovementInputDirection => new Vector3( _freeCamHorizontalMovementInputDirection.x, _freeCamVerticalMovementInputDirection, _freeCamHorizontalMovementInputDirection.y);

    public Vector2 FreeCamLookInputDirection => _freeCamLookInputDirection;


    private void OnDisable()
    {
        _controls.Disable();
    }

    public void TogglePlayerControls(bool state)
    {
        if (state)
        {
            _controls.Player.Enable();
        }
        else
        {
            _controls.Player.Disable();
        }
    }
    
    public void ToggleMenuControls(bool state)
    {
        if (state)
        {
            _controls.Menu.Enable();
        }
        else
        {
            _controls.Menu.Disable();
        }
    }

    public void ToggleFreeCamControls(bool state)
    {
        if (state)
        {
            _controls.FreeCam.Enable();  
        }
        else
        {
            _controls.FreeCam.Disable();;  
        }
        
    }
    
        
}
