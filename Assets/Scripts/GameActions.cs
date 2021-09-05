using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameActions : MonoBehaviour
{
    private LevelManager _levelManager;
    public CutscenesController _velhaCutsceneController;
    public CutscenesController _bofiaCutsceneController;
    public CutscenesController _senhorTonoCutsceneController;
    private FMOD.Studio.EventInstance _earnMoney;
    private FMOD.Studio.EventInstance _loseMoney;
    public PlayerStats playerStats;
    public CustomActionEvent Generic;
    public CustomActionEvent Rape;
    public CustomActionEvent Steal;
    public CustomActionEvent BuyDrugs;
    public CustomActionEvent BuyDrink;
    public CustomActionEvent BuyRaspadinha;
    public CustomActionEvent BuyParadise;
    public CustomActionEvent BuyWeapon;
    public CustomActionEvent SellDrugs;
    public CustomActionEvent GetRobbed;
    public CustomActionEvent GameOver;
    public CustomActionEvent Brothel;
    public CustomActionEvent OralSex;
    public CustomActionEvent VaginalSex;
    public CustomActionEvent AnalSex;
    public CustomActionEvent GetBustedVelha;
    public CustomActionEvent GetBustedBofia;

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
        BuyRaspadinha = new CustomActionEvent();
        BuyParadise = new CustomActionEvent();
        GetRobbed = new CustomActionEvent();
        GameOver = new CustomActionEvent();
        Brothel = new CustomActionEvent();
        OralSex = new CustomActionEvent();
        VaginalSex = new CustomActionEvent();
        AnalSex = new CustomActionEvent();
        GetBustedVelha = new CustomActionEvent();
        GetBustedBofia = new CustomActionEvent();
        Generic.AddListener(DoGeneric);
        Rape.AddListener(DoRape);
        Steal.AddListener(DoSteal);
        BuyDrugs.AddListener(DoBuyDrugs);
        SellDrugs.AddListener(DoSellDrugs);
        BuyDrink.AddListener(DoBuyDrink);
        BuyRaspadinha.AddListener(DoBuyRaspadinha);
        BuyParadise.AddListener(DoBuyParadise);
        BuyWeapon.AddListener(DoBuyWeapon);
        GetRobbed.AddListener(DoGetRobbed);
        GameOver.AddListener(DoGameOver);
        Brothel.AddListener(DoBrothel);
        GetBustedVelha.AddListener(DoGetBustedOnVelha);
        GetBustedBofia.AddListener(DoGetBustedOnBofia);
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
        npcStats.NumberOfTimesBeenRaped++;
        _levelManager.LoadRape();
    }

    public void DoSteal(NPCStats npcStats)
    {
        IncreaseMoney(npcStats.moneyAmount);
        npcStats.NumberOfTimesBeenRobbed++;
        IncreaseWantedLevel();
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

    public void DoBuyParadise(NPCStats npcStats)
    {
        _senhorTonoCutsceneController.StartSenhorTonoCutscene();
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
        if (npcStats.iD == "Bofia")
        {
         GetBustedBofia.Invoke(npcStats);   
        }
    }

    public void DoGetBustedOnBofia(NPCStats npcStats)
    {
        _bofiaCutsceneController.StartBofiaCutscene();
    }

    public void DoGetBustedOnVelha(NPCStats npcStats)
    {
        _velhaCutsceneController.StartVelhaCutscene();
    }

    public void DoVaginalSex(NPCStats npcStats)
    {
        DecreaseMoney(100);
    }

    public void DoOralSex(NPCStats npcStats)
    {
        DecreaseMoney(80);
    }

    public void DoAnalSex(NPCStats npcStats)
    {
        DecreaseMoney(120);
    }

    public void IncreaseWantedLevel()
    {
        if (playerStats.wantedLevel == 3)
        {
            return;
        }
        
        playerStats.wantedLevel++;
    }

    public void DoBrothel(NPCStats npcStats)
    {
        _levelManager.LoadBrothel();
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
        _loseMoney.start();
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

    public void DoBuyRaspadinha(NPCStats _npcStats)
    {
        DecreaseMoney(_npcStats.tradePrices.buy2);
        float randomProbability = UnityEngine.Random.Range(0f, 1f);
        if (randomProbability <= 0.30f)
        {
            randomProbability = Random.Range(0f, 1f);
            if (randomProbability <= 0.30f)
            {
                randomProbability = Random.Range(0f, 1f);
                if (randomProbability <= 0.30f)
                {
                    IncreaseMoney(200);
                    return;
                }
                IncreaseMoney(100);
                return;
            }
            IncreaseMoney(30);
        }
    }
    
    
}

public class CustomActionEvent: UnityEvent<NPCStats>
{

}
