using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NpcAIBehaviour))]
public class VelhaAIBehaviour : MonoBehaviour
{
    public NavMeshAgent _agent;
    public VelhaGatesController _gateController;
    private NpcAIBehaviour _npcBehaviour;
    public NPCDialogueTrigger npcDialogueTrigger;
    public bool hasBeenRaped;
    public Transform outsideWaypoint;
    public string outsideWaypointTag;
    public Transform insideWaypoint;
    public string insideWaypointTag;
    public UnityEvent OpenGateEvent;
    
    private void Start()
    {
        _npcBehaviour = GetComponent<NpcAIBehaviour>();

        if (outsideWaypoint == null)
        {
            outsideWaypoint = GameObject.FindGameObjectWithTag(outsideWaypointTag).GetComponent<Transform>();
        }

        if (insideWaypoint == null)
        {
            insideWaypoint = GameObject.FindGameObjectWithTag(insideWaypointTag).GetComponent<Transform>();
        }

        if (_npcBehaviour.timeController.dayState == TimeController.DayState.Day)
        {
            npcDialogueTrigger.enabled = true;
        }
        else
        {
            npcDialogueTrigger.enabled = false;
        }
        
        _npcBehaviour.timeController.dayStateChange.AddListener(ResetVelhaAvailability);
    }

    private void ResetVelhaAvailability()
    {
        if (_npcBehaviour.timeController.dayState == TimeController.DayState.Day)
        {
            hasBeenRaped = false;   
        }
    }

    [Task]
    void SetDestination()
    {
        if (_npcBehaviour.IsDay)
        {
            _npcBehaviour._currentTarget = outsideWaypoint;
        }

        if (_npcBehaviour.IsNight)
        {
            _npcBehaviour._currentTarget = insideWaypoint;
        }
        
        Task.current.Succeed();
    }

    public void SetPosition(TimeController.DayState dayState)
    {
        if (dayState == TimeController.DayState.Day)
        {
            transform.position = outsideWaypoint.position;
        }
        else
        {
            transform.position = insideWaypoint.position;
        }
    }

    [Task]
    void StartClean()
    {
        npcDialogueTrigger.enabled = true;
        Debug.Log("Start Clean");
        Task.current.Succeed();
    }

    [Task]
    void StopClean()
    {
        npcDialogueTrigger.enabled = false;
        npcDialogueTrigger._npcDialogueReferences.dialoguePrompt.SetActive(false);
        Debug.Log("Stop Clean");
        Task.current.Succeed();
    }

    [Task]
    void OpenGate()
    {
        if (hasBeenRaped)
        {
            Task.current.Succeed();
            return;
        }
        OpenGateEvent.Invoke();
        Task.current.Succeed();
    }
}
