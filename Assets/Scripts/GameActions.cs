using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameActions : MonoBehaviour
{
    private LevelManager _levelManager;
    private FMOD.Studio.EventInstance _earnMoney;
    private FMOD.Studio.EventInstance _loseMoney;
    public PlayerStats playerStats;
    public CustomActionEvent Generic;
    public CustomActionEvent Rape;
    public CustomActionEvent Steal;
    public CustomActionEvent BuyDrugs;
    public CustomActionEvent BuyDrink;
    public CustomActionEvent BuyWeapon;
    public CustomActionEvent SellDrugs;
    public CustomActionEvent GetRobbed;
    public CustomActionEvent GameOver;
    public CustomActionEvent Brodel;
    public CustomActionEvent OralSex;
    public CustomActionEvent VaginalSex;
    public CustomActionEvent AnalSex;

    private void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
        Generic = new CustomActionEvent();
        Rape = new CustomActionEvent();
        Steal = new CustomActionEvent();
        BuyDrugs = new CustomActionEvent();
        BuyWeapon = new CustomActionEvent();
        SellDrugs = new CustomActionEvent();
        BuyDrink = new CustomActionEvent();
        GetRobbed = new CustomActionEvent();
        GameOver = new CustomActionEvent();
        Brodel = new CustomActionEvent();
        OralSex = new CustomActionEvent();
        VaginalSex = new CustomActionEvent();
        AnalSex = new CustomActionEvent();
        Generic.AddListener(DoGeneric);
        Rape.AddListener(DoRape);
        Steal.AddListener(DoSteal);
        BuyDrugs.AddListener(DoBuyDrugs);
        SellDrugs.AddListener(DoSellDrugs);
        BuyDrink.AddListener(DoBuyDrink);
        BuyWeapon.AddListener(DoBuyWeapon);
        GetRobbed.AddListener(DoGetRobbed);
        GameOver.AddListener(DoGameOver);
        Brodel.AddListener(DoBrodel);
    }

    private void Start()
    {
        _earnMoney = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/GanharDinheiro");
        _loseMoney = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/PerderDinheiro");
    }

    public void DoGeneric(NPCStats npcStats)
    {
        
    }
    
    public void DoRape(NPCStats npcStats)
    {
        playerStats.wantedLevel++;
        FindObjectOfType<DialogueTrackerVelha>().UpdateNumberOfRapeInteractions();
        _levelManager.LoadRape();
    }

    public void DoSteal(NPCStats npcStats)
    {
        IncreaseMoney(npcStats.moneyAmount);
        IncreaseWantedLevel();
        FindObjectOfType<DialogueTrackerVelha>().UpdateNumberOfStealInteractions();
    }

    public void DoBuyDrugs(NPCStats npcStats)
    {
        IncreaseDrugsAmount(1);
        DecreaseMoney(npcStats.tradePrices.buy);
    }

    public void DoBuyWeapon(NPCStats npcStats)
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
            DecreaseWantedLevel(playerStats.wantedLevel);
        }
        
        DecreaseDrugsAmount(1);
        IncreaseMoney(npcStats.tradePrices.sell);
    }

    public void DoGetRobbed(NPCStats npcStats)
    {
        if (playerStats.drugsAmount > 0)
        {
            DecreaseDrugsAmount(playerStats.drugsAmount);
            return;
        }
        
        DecreaseMoney(playerStats.moneyAmount);
        
    }

    public void DoGameOver(NPCStats npcStats)
    {
        SceneManager.LoadScene("GameOver");
    }

    public void IncreaseWantedLevel()
    {
        if (playerStats.wantedLevel == 3)
        {
            return;
        }
        
        playerStats.wantedLevel++;
    }

    public void DoBrodel(NPCStats npcStats)
    {
        _levelManager.LoadBrodel();
    }
    

    public void DecreaseWantedLevel(int amount)
    {
        if (playerStats.wantedLevel == 0)
        {
            return;
        }
        
        playerStats.wantedLevel -= amount;
    }

    public void IncreaseMoney(int amount)
    {
        playerStats.moneyAmount += amount;
        _earnMoney.start();
    }

    public void DecreaseMoney(int amount)
    {
        if (playerStats.moneyAmount == 0)
        {
            return;
        }
        
        playerStats.moneyAmount -= amount;
    }

    public void IncreaseDrugsAmount(int amount)
    {
        playerStats.drugsAmount++;
    }

    public void DecreaseDrugsAmount(int amount)
    {
        if (playerStats.drugsAmount == 0)
        {
            return;
        }
        
        playerStats.drugsAmount -= amount;
    }
    
    
}

public class CustomActionEvent: UnityEvent<NPCStats>
{

}
