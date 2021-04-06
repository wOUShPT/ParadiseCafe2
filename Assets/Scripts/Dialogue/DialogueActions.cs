using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class DialogueActions : MonoBehaviour
{
    public CustomActionEvent Generic;
    public CustomActionEvent Rape;
    public CustomActionEvent Steal;
    public CustomActionEvent Buy;
    public CustomActionEvent Sell;

    private void Awake()
    {
        Generic = new CustomActionEvent();
        Rape = new CustomActionEvent();
        Steal = new CustomActionEvent();
        Buy = new CustomActionEvent();
        Sell = new CustomActionEvent();
        Generic.AddListener(DoGeneric);
        Rape.AddListener(DoRape);
        Steal.AddListener(DoSteal);
        Buy.AddListener(DoBuy);
        Sell.AddListener(DoSell);
    }

    private void Start()
    {
        
    }

    public void DoGeneric(PlayerStats playerStats, NPCStats npcStats)
    {
        
    }
    
    public void DoRape(PlayerStats playerStats, NPCStats npcStats)
    {
        playerStats.wantedLevel++;
    }

    public void DoSteal(PlayerStats playerStats, NPCStats npcStats)
    {
        playerStats.moneyAmount += npcStats.moneyAmount;
        playerStats.wantedLevel++;
    }

    public void DoBuy(PlayerStats playerStats, NPCStats npcStats)
    {
        playerStats.drugsAmount += 2;
        playerStats.moneyAmount -= npcStats.tradePrices.buy;
    }

    public void DoSell(PlayerStats playerStats, NPCStats npcStats)
    {
        playerStats.drugsAmount -= 2;
        playerStats.moneyAmount += npcStats.tradePrices.sell;
    }
}

public class CustomActionEvent: UnityEvent<PlayerStats, NPCStats>
{

}
