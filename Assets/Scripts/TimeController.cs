using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    private TextMeshProUGUI _textClock;
    [Range(0,1)]
    public float inGameTime;
    public float durationOfDayInMinutes;
    private int _hours;
    private int _minutes;
    private float _time;
    private float _timePercentage;
    public UnityEvent dayStateChange;
    public bool timeFreeze;
    
    public enum DayState
    {
        Day,
        Night
    }

    private DayState _state;

    public DayState dayState => _state;

    public float TimePercentage => _timePercentage;

    public int hours => _hours;

    public int minutes => _minutes;


    private void OnValidate()
    {
        _timePercentage = inGameTime;
    }

    void Awake()
    {
        _textClock = FindObjectOfType<HUDReferences>().clock.GetComponent<TextMeshProUGUI>();
        _timePercentage = inGameTime;
        ConvertToHoursMinutes();

        if (_hours >= 19)
        {
            Debug.Log("Night");
            _state = DayState.Night;
            return;
        }

        if (_hours < 6)
        {
            Debug.Log("Night");
            _state = DayState.Night;
        }
        else
        {
            Debug.Log("Day");
            _state = DayState.Day;
        }
    }
    
    void Update()
    {
        if (!timeFreeze)
        {
            _timePercentage = Mathf.Repeat(_timePercentage + Time.deltaTime / (durationOfDayInMinutes*60), 1);
            //Debug.Log(_timePercentage);
            //_timePercentage += Time.deltaTime/(60* durationOfDayInMinutes);
        }

        ConvertToHoursMinutes();
        
        if (_hours == 19)
        {
            Debug.Log("Night");
            _state = DayState.Night;
            dayStateChange.Invoke();
        }

        if (_hours == 6)
        {
            Debug.Log("Day");
            _state = DayState.Day;
            dayStateChange.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            durationOfDayInMinutes--;
            Mathf.Clamp(durationOfDayInMinutes, 1, Mathf.Infinity);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            durationOfDayInMinutes++;
            Mathf.Clamp(durationOfDayInMinutes, 1, Mathf.Infinity);
        }

        string time = string.Format("{0:00}:{1:00}", _hours, _minutes);
        
        _textClock.text = time;
    }
    

    void ConvertToHoursMinutes()
    {
        _hours = (int)(_timePercentage*24);
        _minutes = (int)(((_timePercentage*24) - _hours) *60);
    }
}
