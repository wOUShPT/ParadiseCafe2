using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BrothelSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance _idleSound;
    private FMOD.Studio.EventInstance _oralSound;
    private FMOD.Studio.EventInstance _vaginalSound;
    private FMOD.Studio.EventInstance _analSound;
    private FMOD.Studio.EventInstance _reginaldoSound;
    void Start()
    {
        _idleSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneIdle");
        _oralSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneOral");
        _vaginalSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneVaginal");
        _analSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneAnal");
        _reginaldoSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SexSceneReginaldo");
    }

    public void PlayIdleSound()
    {
        _idleSound.start();
    }
    
    public void StopIdleSound()
    {
        _idleSound.stop(STOP_MODE.IMMEDIATE);
    }
    
    public void PlayOralSound()
    {
        _oralSound.start();
    }

    public void StopOralSound()
    {
        _oralSound.stop(STOP_MODE.IMMEDIATE);
    }

    public void PlayVaginalSound()
    {
        _vaginalSound.start();
    }

    public void StopVaginalSound()
    {
        _vaginalSound.stop(STOP_MODE.IMMEDIATE);
    }

    public void PlayAnalSound()
    {
        _analSound.start();
    }

    public void StopAnalSound()
    {
        _analSound.stop(STOP_MODE.IMMEDIATE);
    }

    public void PlayReginaldoSound()
    {
        _reginaldoSound.start();
    }

    public void StopReginaldoSound()
    {
        _reginaldoSound.stop(STOP_MODE.IMMEDIATE);
    }

    public void StopSounds()
    {
        _idleSound.stop(STOP_MODE.IMMEDIATE);
        _oralSound.stop(STOP_MODE.IMMEDIATE);
        _vaginalSound.stop(STOP_MODE.IMMEDIATE);
        _analSound.stop(STOP_MODE.IMMEDIATE);
        _reginaldoSound.stop(STOP_MODE.IMMEDIATE);
    }
}
