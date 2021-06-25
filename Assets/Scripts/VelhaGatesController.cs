using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelhaGatesController : MonoBehaviour
{
    public Animator leftGateAnimator;
    public Animator rightGateAnimator;
    private VelhaAIBehaviour _velhaAIBehaviour;

    private void Start()
    {
        _velhaAIBehaviour = FindObjectOfType<VelhaAIBehaviour>();
        _velhaAIBehaviour.OpenGateEvent.AddListener(OpenGate);
    }

    public void OpenGate()
    {
        StartCoroutine(OpenGateAnimation());
    }

    IEnumerator OpenGateAnimation()
    {
        leftGateAnimator.SetTrigger("Open");
        rightGateAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(10f);
        leftGateAnimator.SetTrigger("Close");
        rightGateAnimator.SetTrigger("Close");
    }
}
