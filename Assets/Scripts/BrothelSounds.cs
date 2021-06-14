using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BrothelSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance _oralSound;
    private FMOD.Studio.EventInstance _vaginalSound;
    private FMOD.Studio.EventInstance _analSound;
    void Start()
    {
        _oralSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneOral");
        _vaginalSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneVaginal");
        _analSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneAnal");
    }

    public void PlayOralSound()
    {
        _oralSound.start();
    }

    public void PlayVaginalSound()
    {
        _vaginalSound.start();
    }

    public void PlayAnalSound()
    {
        _analSound.start();
    }

    public void StopSounds()
    {
        _oralSound.stop(STOP_MODE.IMMEDIATE);
        _vaginalSound.stop(STOP_MODE.IMMEDIATE);
        _analSound.stop(STOP_MODE.IMMEDIATE);
    }
}
