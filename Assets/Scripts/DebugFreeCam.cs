using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


#if UNITY_EDITOR

public class DebugFreeCam : MonoBehaviour
{
    private InputManager _inputManager;
    private HUDReferences _hudReferences; 
    public CinemachineVirtualCamera camera;
    [Range(0,1)]
    public float xLookSensitivity;
    [Range(0,1)]
    public float yLookSensitivity;
    public float MovementSpeed;
    private bool cameraState;
    
    private void Start()
    {
        cameraState = false;
        _inputManager = FindObjectOfType<InputManager>();
        _hudReferences = FindObjectOfType<HUDReferences>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            cameraState = !cameraState;
            ActivateCamera(cameraState);
        }

        if (cameraState)
        {
            Vector3 movementDirection = _inputManager.FreeCamMovementInputDirection;
            movementDirection = Camera.main.transform.TransformDirection(movementDirection);
            movementDirection *= MovementSpeed;
            transform.position += movementDirection;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + -_inputManager.FreeCamLookInputDirection.y * yLookSensitivity, transform.rotation.eulerAngles.y + _inputManager.FreeCamLookInputDirection.x * xLookSensitivity, 0);
        }
    }

    private void ActivateCamera(bool state)
    {
        if (state)
        {
            _inputManager.TogglePlayerControls(false);
            _hudReferences.HUDPanel.SetActive(false);
            camera.Priority = 200;
        }
        else
        {
            _inputManager.TogglePlayerControls(true);
            _hudReferences.HUDPanel.SetActive(true);
            camera.Priority = 0;
        }
    }
}

#endif
