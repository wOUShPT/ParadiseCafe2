using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyIncome : MonoBehaviour
{
    public PlayerStats _playerStats;
    public float showIncomeDisplayTime;
    public float IncomeTimeHour;
    public float IncomeTimeMinute;
    private TimeController _timeController;
    public GameObject dailyIncome;
    private bool waitingForIncome;

    void Start()
    {
        _timeController = FindObjectOfType<TimeController>();
        waitingForIncome = true;
    }
    
    void Update()
    {
        if (_timeController.hours == IncomeTimeHour && _timeController.minutes == IncomeTimeMinute && waitingForIncome)
        {
            _playerStats.moneyAmount += 10;
            StartCoroutine(ShowGains());
        }
    }


    IEnumerator ShowGains()
    {
        waitingForIncome = false;
        dailyIncome.SetActive(true);
        yield return new WaitForSeconds(showIncomeDisplayTime);
        dailyIncome.SetActive(false);
        yield return new WaitUntil(() => _timeController.minutes != IncomeTimeMinute);
        waitingForIncome = true;
    }
}
