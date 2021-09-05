using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NpcAIBehaviour))]
public class DomAIBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private NpcAIBehaviour _npcBehaviour;
    public NPCDialogueTrigger npcDialogueTrigger;
    
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
        if (_npcBehaviour.IsNight)
        {
            _npcBehaviour._currentTarget = outsideWaypoint;
        }

        if (_npcBehaviour.IsDay)
        {
            _npcBehaviour._currentTarget = insideWaypoint;
        }
        
        Task.current.Succeed();
    }

    [Task]
    void StartWaiting()
    {
        Debug.Log("Waiting");
        Task.current.Succeed();
    }

    [Task]
    void StopWaiting()
    {
        Debug.Log("StopWaiting");
        Task.current.Succeed();
    }
    
    void Setup()
    {
        SetDestination();
        
        _npcBehaviour.SetRotation();

        if (_npcBehaviour.IsNight)
        {
            StartWaiting();
        }

        if (_npcBehaviour.IsDay)
        {
            StopWaiting();
        }
    }
}
