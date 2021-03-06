using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]

public class ThirdPersonController : MonoBehaviour
{
    private InputManager _inputManager;
    private LevelManager _LevelManager;
    private DialogueManager _dialogueManager;
    public float moveSpeed = 7.5f;
    public float rotationSpeed;
    public Vector3 moveDirection;
    private Vector3 _move;
    private float _horizontalVelocity;
    public bool canMove;
    public bool canRotate;
    public float allowPlayerRotation;
    private CharacterController _characterController;
    public CinemachineFreeLook cameraCinemachine;
    private Transform playerCameraTransform;

    public float Velocity => _horizontalVelocity;
    

    void Start()
    {
        _LevelManager = FindObjectOfType<LevelManager>();
        _inputManager = FindObjectOfType<InputManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (canMove)
        {
            InputMagnitude();
        }
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

    public void RecenterCamera()
    {
        StartCoroutine(RecenterCineMachine());
    }

    IEnumerator RecenterCineMachine()
    {
        cameraCinemachine.m_RecenterToTargetHeading.m_enabled = true;
        yield return new WaitForEndOfFrame();
        cameraCinemachine.m_RecenterToTargetHeading.m_enabled = false;
        yield return null;
    }

    void UpdateAnimation()
    {
        
    }


    public void FreezePlayer(bool state)
    {
        if (state)
        {
            canMove = false;
            _horizontalVelocity = 0;
        }
        else
        {
            canMove = true;
        }
    }
}
