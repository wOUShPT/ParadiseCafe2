using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyIncome : MonoBehaviour
{
    public PlayerStats _playerStats;
    private TimeController _timeController;
    public GameObject dailyIncome;
    public float notificationShowTime;
    private bool incoming;
    
    void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        incoming = false;
    }
    
    void Update()
    {
        if (_timeController.timePercentage >= 0.4f && _timeController.timePercentage < 0.41f && !incoming)
        {
            _playerStats.moneyAmount += 10;
            StartCoroutine(ShowGains());
        }
    }


    IEnumerator ShowGains()
    {
        incoming = true;
        dailyIncome.SetActive(true);
        yield return new WaitForSeconds(notificationShowTime);
        dailyIncome.SetActive(false);
        incoming = false;
    }
}
