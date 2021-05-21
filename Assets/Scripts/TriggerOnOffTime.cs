using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnOffTime : MonoBehaviour
{
    private TimeController _timeController;
    public bool OnDay;
    public bool OnNight;
    public BoxCollider _trigger;
    
    private void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(ToggleTrigger);
    }

    void ToggleTrigger()
    {
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            _trigger.enabled = OnDay;
        }

        if (_timeController.dayState == TimeController.DayState.Night)
        {
            _trigger.enabled = OnNight;
        }
    }
}
