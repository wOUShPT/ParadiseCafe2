using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SetPlayerStats : MonoBehaviour
{
    public PlayerStats playerStats;
    public int moneyAmount;
    public int drugsAmount;
    public bool hasWeapon;
    [Range(0,3)]
    public int wantedLevel;

    private void Awake()
    {
        ResetStats();
    }

    public void ResetStats()
    {
        playerStats.moneyAmount = moneyAmount;
        playerStats.drugsAmount = drugsAmount;
        playerStats.hasWeapon = hasWeapon;
        playerStats.wantedLevel = wantedLevel;
    }
}
