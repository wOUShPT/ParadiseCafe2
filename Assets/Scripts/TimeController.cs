using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI _textClock;
    [Range(0,1)]
    public float dayTime;
    public float durationOfDayInMinutes;
    private int _hours;
    private int _minutes;
    private double _time;
    public bool timeFreeze;

    public float timePercentage => (float)_time;

    public int hours => _hours;

    public int minutes => _minutes;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        _hours = 0;
        _minutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time = dayTime;
        
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

        string time = string.Format("{0:00}:{1:00}", _hours, _minutes);
        _textClock.text = time;
    }
}
