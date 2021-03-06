using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NpcAIBehaviour))]
[RequireComponent(typeof(NavMeshAgent))]
public class BofiaAIBehaviour : MonoBehaviour
{
    public List<Transform> waypointsList;

    private NavMeshAgent _agent;

    private NpcAIBehaviour _npcBehaviour;

    public float minimumWaitTime;

    public float maximumWaitTime;

    private float _waitTime;

    private float _waitTimer;

    private int _currentWaypointIndex;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _npcBehaviour = GetComponent<NpcAIBehaviour>();
    }

    [Task]
    void SetWaypoints()
    {
        if (!_agent.hasPath)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypointsList.Count)
            {
                _currentWaypointIndex = 0;
            }

            _npcBehaviour._currentTarget = waypointsList[_currentWaypointIndex];
        }
        Task.current.Succeed();
    }

    [Task]
    void ResetTimer()
    {
        _waitTime = Random.Range(minimumWaitTime, maximumWaitTime);
        _waitTimer = 0;
        Task.current.Succeed();
    }
    
    [Task]
    void RunTimer()
    {
        _waitTimer += Time.deltaTime;
        if (_waitTimer >= _waitTime)
        {
            Task.current.Succeed();
        }
    }
    
    #if UNITY_EDITOR
    
    private void Update()
    {
        Vector3 npcPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 targetPosition = new Vector3(_npcBehaviour._currentTarget.position.x, transform.position.y+1, _npcBehaviour._currentTarget.position.z);
        Debug.DrawLine(npcPosition, npcPosition - targetPosition, Color.green);
    }
    
    #endif
}
