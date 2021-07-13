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
    
    public void StopIdleSound(STOP_MODE mode)
    {
        _idleSound.stop(mode);
    }
    
    public void PlayOralSound()
    {
        _oralSound.start();
    }

    public void StopOralSound(STOP_MODE mode)
    {
        _oralSound.stop(mode);
    }

    public void PlayVaginalSound()
    {
        _vaginalSound.start();
    }

    public void StopVaginalSound(STOP_MODE mode)
    {
        _vaginalSound.stop(mode);
    }

    public void PlayAnalSound()
    {
        _analSound.start();
    }

    public void StopAnalSound(STOP_MODE mode)
    {
        _analSound.stop(mode);
    }

    public void PlayReginaldoSound()
    {
        _reginaldoSound.start();
    }

    public void StopReginaldoSound(STOP_MODE mode)
    {
        _reginaldoSound.stop(mode);
    }

    public void StopSounds(STOP_MODE mode)
    {
        _idleSound.stop(mode);
        _oralSound.stop(mode);
        _vaginalSound.stop(mode);
        _analSound.stop(mode);
        _reginaldoSound.stop(mode);
    }
}
