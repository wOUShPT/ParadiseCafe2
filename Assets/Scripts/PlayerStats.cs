using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerStats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float moneyAmount;
    public int drugsAmount;
    public bool hasWeapon;
    public int wantedLevel;
}
