using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PlayerAudioHandler : MonoBehaviour
{
    private FMOD.Studio.EventInstance walkExteriorSound;
    private FMOD.Studio.EventInstance fapSound;

    private void Start()
    {
        walkExteriorSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/AndarExterior");
        fapSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Fap");
    }

    public void PlayWalkSound()
    {
        walkExteriorSound.start();
    }

    public void PlayFapSound()
    {
        fapSound.start();
    }
}
