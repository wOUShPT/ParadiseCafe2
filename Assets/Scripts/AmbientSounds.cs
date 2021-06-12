using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance _cricketsSound;
    private FMOD.Studio.EventInstance _birdsSound;
    private TimeController _timeController;
    void Start()
    {
        _cricketsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Grilos");
        _birdsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Pássaros");
        _cricketsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        _birdsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(ChangeState);
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
