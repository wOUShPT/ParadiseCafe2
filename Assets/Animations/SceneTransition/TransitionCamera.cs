using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCamera : MonoBehaviour
{
    public Camera transitionCamera;

    void Update()
    {
        transitionCamera.transform.position = Camera.main.transform.position;
        transitionCamera.transform.rotation = Camera.main.transform.rotation;
    }
}
