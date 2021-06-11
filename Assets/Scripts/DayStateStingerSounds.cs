using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayStateStingerSounds : MonoBehaviour
{
    private TimeController _timeController;
    private FMOD.Studio.EventInstance _dayStateSound;
    private FMOD.Studio.EventInstance _nightStateSound;

    void Start()
    {
        _dayStateSound = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/Amanhecer");
        _nightStateSound = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/Anoitecer");
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(PlayDayNightStateSound);
    }

    void PlayDayNightStateSound()
    {
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            _dayStateSound.start();
        }

        if (_timeController.dayState == TimeController.DayState.Night)
        {
            _nightStateSound.start();
        }
    }
}
