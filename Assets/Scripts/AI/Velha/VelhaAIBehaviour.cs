using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NpcAIBehaviour))]
public class VelhaAIBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private NpcAIBehaviour _npcBehaviour;
    public DialogueTrigger _dialogueTrigger;
    
    public Transform outsideWaypoint;
    public string outsideWaypointTag;

    public Transform insideWaypoint;
    public string insideWaypointTag;
    
    private void Start()
    {
        _npcBehaviour = GetComponent<NpcAIBehaviour>();
        _agent = GetComponent<NavMeshAgent>();
        
        if (outsideWaypoint == null)
        {
            outsideWaypoint = GameObject.FindGameObjectWithTag(outsideWaypointTag).GetComponent<Transform>();
        }

        if (insideWaypoint == null)
        {
            insideWaypoint = GameObject.FindGameObjectWithTag(insideWaypointTag).GetComponent<Transform>();
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

    [Task]
    void StartClean()
    {
        Debug.Log("Cleaning");
        Task.current.Succeed();
    }

    [Task]
    void StopClean()
    {
        Debug.Log("StopCleaning");
        Task.current.Succeed();
    }
}
