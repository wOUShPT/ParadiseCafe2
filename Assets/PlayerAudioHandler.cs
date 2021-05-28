using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PlayerAudioHandler : MonoBehaviour
{
    private FMOD.Studio.EventInstance walkSoundExterior;

    private void Start()
    {
        walkSoundExterior = FMODUnity.RuntimeManager.CreateInstance("event:/AndarExterior");
    }

    void Update()
    {
        
    }

    public void PlayWalkSound()
    {
        walkSoundExterior.start();
    }
}
