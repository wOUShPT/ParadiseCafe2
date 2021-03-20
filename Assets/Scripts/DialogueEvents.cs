using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class DialogueEvents : MonoBehaviour
{
    public UnityEvent Rape;
    public UnityEvent Steal;
    public UnityEvent Buy;
    public UnityEvent Sell;

    private void Awake()
    {
        Rape = new UnityEvent();
        Steal = new UnityEvent();
        Buy = new UnityEvent();
        Sell = new UnityEvent();
    }

    public void DoRape()
    {
        Rape.Invoke();
    }

    public void DoSteal()
    {
        Steal.Invoke();
    }

    public void DoBuy()
    {
        Buy.Invoke();
    }

    public void DoSell()
    {
        Sell.Invoke();
    }
}
