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

    private bool inDialogue;

    private Quaternion _currentNpcOrientation;
    
    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        isDay = false;
        isNight = false;
        
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
        
        dialogueManager.startedDialogue.AddListener(StopMovement);
        dialogueManager.endedDialogue.AddListener(ResumeMovement);


    }

    [Task]
    void GoToDestination()
    {
        _agent.SetDestination(_currentTarget.position);
        
        Task.current.Succeed();
    }

    [Task]
    void CheckArrival()
    {
        float distanceToTarget = Vector3.Distance(_currentTarget.position, transform.position);

        if (distanceToTarget < 0.1f)
        {
            Task.current.Succeed();
        }
    }
    
    void ChangeGoal()
    {
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
    }
    
    public void StopMovement()
    {
        if (dialogueManager.CurrentNPC == gameObject)
        {
            inDialogue = true;
            StartCoroutine(ForceStop());
            Transform playerTransform = FindObjectOfType<ThirdPersonController>().transform;
            _currentNpcOrientation = transform.rotation;
            transform.LookAt(playerTransform);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
    }

    public void ResumeMovement()
    {
        if (dialogueManager.CurrentNPC == gameObject)
        {
            inDialogue = false;
        }
    }

    IEnumerator ForceStop()
    {
        while (inDialogue)
        {
            _agent.isStopped = true;
            yield return null;
        }
        
        _agent.isStopped = false;
        transform.rotation = _currentNpcOrientation;
    }

    [Task]
    public void SetRotation()
    {
        transform.rotation = _currentTarget.rotation;
        Task.current.Succeed();
    }
}