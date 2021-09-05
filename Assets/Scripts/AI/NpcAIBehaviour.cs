using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Unity.Mathematics;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NpcAIBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    [Task]
    private bool isDay;
    
    [Task] 
    private bool isNight;

    public bool IsDay => isDay;

    public bool IsNight => isNight;
    
    public Transform _currentTarget;
    
    public TimeController timeController;

    private DialogueManager dialogueManager;

    [Task]
    public bool inDialogue;

    [Task]
    public bool hasArrived;

    private Quaternion _currentNpcOrientation;
    
    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        isDay = false;
        isNight = false;
        inDialogue = false;
        
        timeController = FindObjectOfType<TimeController>();
        timeController.dayStateChange.AddListener(ChangeGoal);

        if (timeController.dayState == TimeController.DayState.Day)
        {
            isDay = true;
            isNight = false;
        }

        if (timeController.dayState == TimeController.DayState.Night)
        {
            isDay = false;
            isNight = true;
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        
        //dialogueManager.startedDialogue.AddListener(StopMovement);
        //dialogueManager.endedDialogue.AddListener(ResumeMovement);


    }

    [Task]
    void GoToDestination()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_currentTarget.position);
        
        Task.current.Succeed();
    }

    [Task]
    void CheckArrival()
    {
        float distanceToTarget = Vector3.Distance(_currentTarget.position, transform.position);

        if (distanceToTarget < 0.1f)
        {
            hasArrived = true;
            Task.current.Succeed();
        }
    }
    
    void ChangeGoal()
    {
        if (timeController.dayState == TimeController.DayState.Day)
        {
            hasArrived = false;
            isDay = true;
            isNight = false;
        }

        if (timeController.dayState == TimeController.DayState.Night)
        {
            hasArrived = false;
            isDay = false;
            isNight = true;
        }
    }
    
    [Task]
    private void StopMovement()
    {
        _agent.isStopped = true;
        Task.current.Succeed();
    }

    [Task]
    private void ResumeMovement()
    {
        _agent.isStopped = false;
        Task.current.Succeed();
    }
    

    [Task]
    public void TurnToPlayer()
    {
        Transform playerTransform = FindObjectOfType<ThirdPersonController>().transform;
        _currentNpcOrientation = transform.rotation;
        Vector3 playerDirection = playerTransform.position - transform.position;
        transform.LookAt(playerTransform, Vector3.up);
    }

    [Task]
    public void SetRotation()
    {
        transform.rotation = _currentTarget.rotation;
        Task.current.Succeed();
    }
}
