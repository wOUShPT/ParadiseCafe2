using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCStats", menuName = "Scriptable Objects/NPC Stats")]
public class NPCStats : ScriptableObject
{
    public string iD;
    public float moneyAmount;
    public int aggressiveness;
    public bool hasWeapon;
    public TradePrices tradePrices;
    
    [System.Serializable]
    public struct TradePrices
    {
        public float buy;
        public float sell;
        public float buyPistol;
    }
}
