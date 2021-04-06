using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD: MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI wantedLevel;
    public TextMeshProUGUI hasWeapon;
    public PlayerStats playerStats;
    void Update()
    {
        moneyText.SetText(playerStats.moneyAmount + " €");
        wantedLevel.SetText("Nível " + playerStats.wantedLevel + " de procurado");
        if (playerStats.hasWeapon)
        {
            hasWeapon.text = "Tens uma arma";
        }
        else
        {
            hasWeapon.text = "Não tens uma arma";
        }
    }
}