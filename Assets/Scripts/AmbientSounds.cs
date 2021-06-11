using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance _cricketsSound;
    private FMOD.Studio.EventInstance _birdsSound;
    public TimeController _timeController;
    void Start()
    {
        _cricketsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Grilos");
        _birdsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Pássaros");
        _timeController = FindObjectOfType<TimeController>();
        ChangeState();
    }

    void ChangeState()
    {
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            _birdsSound.start();
            _cricketsSound.stop(STOP_MODE.ALLOWFADEOUT);
        }

        if (_timeController.dayState == TimeController.DayState.Night)
        {
            _cricketsSound.start();
            _birdsSound.stop(STOP_MODE.ALLOWFADEOUT);
        }
            

    }
}
