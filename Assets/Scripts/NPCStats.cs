using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCStats", menuName = "Scriptable Objects/NPC Stats")]
public class NPCStats : ScriptableObject
{
    public string iD;
    public int moneyAmount;
    public int aggressiveness;
    public bool hasWeapon;
    [Space]
    public TradePrices tradePrices;
    
    [System.Serializable]
    public struct TradePrices
    {
        public int buy;
        public int buy2;
        public int sell;
        public int buyPistol;
    }
    
    [Space]
    public int NumberOfInteractionsMade;
    public int NumberOfTimesBeenRaped;
    public int NumberOfTimesBeenRobbed;
}
