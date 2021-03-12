using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class ThirdPersonController : MonoBehaviour
{
    public InputManager _inputManager;
    public float moveSpeed = 7.5f;
    public float rotationSpeed;
    public Vector3 moveDirection;
    private Vector3 _move;
    private float _horizontalVelocity;
    public bool canRotate;
    public float allowPlayerRotation;
    private CharacterController _characterController;
    private Transform playerCameraTransform;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        InputMagnitude();
    }

    void PlayerMoveAndRotate()
    {
        Vector3 cameraForward = playerCameraTransform.forward;
        Vector3 cameraRight = playerCameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDirection = cameraForward * _inputManager.MovementInputDirection.y + cameraRight * _inputManager.MovementInputDirection.x;

        if (canRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
        }
    }

    void InputMagnitude()
    {
        
        _horizontalVelocity = _inputManager.MovementInputDirection.sqrMagnitude;

        if (_horizontalVelocity > allowPlayerRotation)
        {
            PlayerMoveAndRotate();
        }

        _characterController.SimpleMove(transform.forward * _horizontalVelocity * moveSpeed);
    }
}
