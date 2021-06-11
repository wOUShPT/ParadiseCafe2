using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;

public class VelhaCasaAudioController : MonoBehaviour
{
    private TimeController _timeController;
    public GameObject velhaMusicExteriorTrigger;
    void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(PlayVelhaMusicExterior);
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            velhaMusicExteriorTrigger.SetActive(true);
        }
    }
    

    void PlayVelhaMusicExterior()
    {
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            velhaMusicExteriorTrigger.SetActive(true);
        }
        
        if(_timeController.dayState == TimeController.DayState.Night)
        {
            velhaMusicExteriorTrigger.SetActive(false);
        }
    }
}
