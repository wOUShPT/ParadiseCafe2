using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PlayerAudioHandler : MonoBehaviour
{
    private FMOD.Studio.EventInstance stepsSound;

    private void Start()
    {
        stepsSound = FMODUnity.RuntimeManager.CreateInstance("events/")
    }

    void Update()
    {
        
    }

    void PlayStepsSound()
    {
        stepsSound.
    }
}
