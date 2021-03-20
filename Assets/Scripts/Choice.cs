using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Choice
{
    public string choiceText;

    public Dialogue nextDialogue;

    public Action action;
    
    public enum Action
    {
        BuyDrugs,
        SellDrugs,
        BuyPistol,
        Steal,
        Rape
    } 
}


