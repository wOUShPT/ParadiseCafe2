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
    private double _time;
    public UnityEvent dayStateChange;
    public bool timeFreeze;
    
    public enum DayState
    {
        Day,
        Night
    }

    private DayState _state;

    public DayState dayState => _state;
    

    public float timePercentage => (float)_time;

    public int hours => _hours;

    public int minutes => _minutes;
    
    void Awake()
    {
        _textClock = FindObjectOfType<HUDReferences>().clock.GetComponent<TextMeshProUGUI>();
        _time = inGameTime;
        _hours = (int)((24*_time)/durationOfDayInMinutes);
        _minutes = (int)(60 * (((24*_time)/durationOfDayInMinutes) - _hours));
        
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
            _time += Time.deltaTime/(60*durationOfDayInMinutes);
        }

        if (_time >= 1)
        {
            _time = 0;
        }

        _hours = (int)((24*_time)/durationOfDayInMinutes);
        _minutes = (int)(60 * (((24*_time)/durationOfDayInMinutes) - _hours));

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
            Mathf.Clamp(durationOfDayInMinutes, 0, Mathf.Infinity);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            durationOfDayInMinutes++;
            Mathf.Clamp(durationOfDayInMinutes, 0, Mathf.Infinity);
        }

        string time = string.Format("{0:00}:{1:00}", _hours, _minutes);
        
        _textClock.text = time;
    }
}
