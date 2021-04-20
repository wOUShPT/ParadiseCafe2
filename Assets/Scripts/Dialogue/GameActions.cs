using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class GameActions : MonoBehaviour
{
    public PlayerStats playerStats;
    public CustomActionEvent Generic;
    public CustomActionEvent Rape;
    public CustomActionEvent Steal;
    public CustomActionEvent BuyDrugs;
    public CustomActionEvent BuyDrink;
    public CustomActionEvent BuyPistol;
    public CustomActionEvent SellDrugs;

    private void Awake()
    {
        Generic = new CustomActionEvent();
        Rape = new CustomActionEvent();
        Steal = new CustomActionEvent();
        BuyDrugs = new CustomActionEvent();
        BuyPistol = new CustomActionEvent();
        SellDrugs = new CustomActionEvent();
        Generic.AddListener(DoGeneric);
        Rape.AddListener(DoRape);
        Steal.AddListener(DoSteal);
        BuyDrugs.AddListener(DoBuyDrugs);
        SellDrugs.AddListener(DoSellDrugs);
    }

    private void Start()
    {
        
    }

    public void DoGeneric(NPCStats npcStats)
    {
        
    }
    
    public void DoRape(NPCStats npcStats)
    {
        playerStats.wantedLevel++;
    }

    public void DoSteal(NPCStats npcStats)
    {
        IncreaseMoney(npcStats.moneyAmount);
        IncreaseWantedLevel();
    }

    public void DoBuyDrugs(NPCStats npcStats)
    {
        IncreaseDrugsAmount(1);
        DecreaseMoney(npcStats.tradePrices.buy);
    }

    public void DoBuyGun(NPCStats npcStats)
    {
        DecreaseMoney(npcStats.tradePrices.buyPistol);
        playerStats.hasWeapon = true;
    }

    public void DoBuyDrink(NPCStats npcStats)
    {
        DecreaseMoney(npcStats.tradePrices.buy);
    }
    
    public void DoSellDrugs(NPCStats npcStats)
    {
        if (npcStats.iD == "Bofia")
        {
            DecreaseWantedLevel();
        }
        
        DecreaseDrugsAmount(1);
        IncreaseMoney(npcStats.tradePrices.sell);
    }

    public void IncreaseWantedLevel()
    {
        playerStats.wantedLevel++;
    }

    public void DecreaseWantedLevel()
    {
        playerStats.wantedLevel--;
    }

    public void IncreaseMoney(float amount)
    {
        playerStats.moneyAmount += amount;
    }

    public void DecreaseMoney(float amount)
    {
        playerStats.moneyAmount -= amount;
    }

    public void IncreaseDrugsAmount(int amount)
    {
        playerStats.drugsAmount++;
    }

    public void DecreaseDrugsAmount(int amount)
    {
        playerStats.drugsAmount--;
    }
}

public class CustomActionEvent: UnityEvent<NPCStats>
{

}
