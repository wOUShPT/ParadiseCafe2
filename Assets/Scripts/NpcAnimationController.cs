using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class NpcAnimationController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = transform.parent.GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            StartCoroutine(UpdateWalk());
        }
        
    }
    

    public void EnterInteraction()
    {
        _animator.SetTrigger("EnterInteraction");
    }

    public void ExitInteraction()
    {
        _animator.SetTrigger("ExitInteraction");
    }

    IEnumerator UpdateWalk()
    {
        while (true)
        {
            _animator.SetFloat("Velocity", _agent.velocity.magnitude);
            yield return null;
        }
    }
}
