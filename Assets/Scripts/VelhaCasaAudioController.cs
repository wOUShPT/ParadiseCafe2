using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;

public class VelhaCasaAudioController : MonoBehaviour
{
    private TimeController _timeController;
    private FMOD.Studio.EventInstance _velhaMusic;
    private VelhaAIBehaviour _velhaAIBehaviour;
    void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        _velhaMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Musica/VelhaMusicExterior");
        _velhaMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            _velhaMusic.start();
        }
        _timeController.dayStateChange.AddListener(PlayVelhaMusicExterior);
        _velhaAIBehaviour = FindObjectOfType<VelhaAIBehaviour>();
    }
    

    public void PlayVelhaMusicExterior()
    {
        if (_velhaAIBehaviour.hasBeenRaped)
        {
            _velhaMusic.stop(STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            if (_timeController.dayState == TimeController.DayState.Day)
            {
                _velhaMusic.start(); 
            }
        
            if(_timeController.dayState == TimeController.DayState.Night)
            {
                _velhaMusic.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
