using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD: MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI wantedLevel;
    public TextMeshProUGUI hasWeapon;
    public TextMeshProUGUI drugsAmount;
    public PlayerStats playerStats;
    void Update()
    {
        moneyText.SetText(playerStats.moneyAmount + " €");
        wantedLevel.SetText("Nível de procurado: " + playerStats.wantedLevel);
        if (playerStats.hasWeapon)
        {
            hasWeapon.text = "Tens uma arma";
        }
        else
        {
            hasWeapon.text = "Não tens uma arma";
        }

        if (playerStats.drugsAmount > 0)
        {
            drugsAmount.SetText("Tens " + playerStats.drugsAmount + " doses de droga");
        }
        else
        {
            drugsAmount.text = "Não tens droga";
        }
    }
}