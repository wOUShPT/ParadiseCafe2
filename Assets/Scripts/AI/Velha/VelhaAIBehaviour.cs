using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NpcAIBehaviour))]
public class VelhaAIBehaviour : MonoBehaviour
{
    public NavMeshAgent _agent;
    private NpcAIBehaviour _npcBehaviour;
    public NPCDialogueTrigger npcDialogueTrigger;
    public Transform outsideWaypoint;
    public string outsideWaypointTag;

    public Transform insideWaypoint;
    public string insideWaypointTag;
    
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
        if (!npcDialogueTrigger.enabled)
        {
            //npcDialogueTrigger.enabled = true;
        }
        Task.current.Succeed();
    }

    [Task]
    void StopClean()
    {
        if (npcDialogueTrigger.enabled)
        {
            //npcDialogueTrigger.enabled = false;
        }
        Task.current.Succeed();
    }

    void Setup()
    {
        SetDestination();
        
        _npcBehaviour.SetRotation();

        if (_npcBehaviour.IsNight)
        {
            StopClean();
        }

        if (_npcBehaviour.IsDay)
        {
            StartClean();
        }
    }
}
