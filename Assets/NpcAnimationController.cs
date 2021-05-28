using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NpcAnimationController : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    

    public void EnterInteraction()
    {
        _animator.SetTrigger("EnterInteraction");
    }

    public void ExitInteraction()
    {
        _animator.SetTrigger("ExitInteraction");
    }

}
