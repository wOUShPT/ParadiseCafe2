using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelhaGatesController : MonoBehaviour
{
    public Animator leftGateAnimator;
    public Animator rightGateAnimator;
    private TimeController _timeController;

    private void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(() => StartCoroutine(OpenGate()));
    }

    IEnumerator OpenGate()
    {
        leftGateAnimator.SetTrigger("Open");
        rightGateAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(10f);
        leftGateAnimator.SetTrigger("Close");
        rightGateAnimator.SetTrigger("Close");
    }
}
