using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newDialogue", menuName = "Scriptable Objects/Dialogue", order = 1)]
public class Dialogue: ScriptableObject
{
    public enum TypeProp {Normal = 0, Branch = 1}
    
    [SerializeField]
    private Dialogue.TypeProp type = TypeProp.Normal;
    
    [SerializeField]
    private string characterName;

    [SerializeField]
    private Dialogue nextDialogue;
    
    [SerializeField, TextArea(3, 19)]
    private string sentence;
    
    [SerializeField]
    private Choice[] choices = new Choice[4];

    public TypeProp Type => type;
    
    public string CharacterName => characterName;

    public Dialogue NextDialogue => nextDialogue;

    public string Sentence => sentence;

    public Choice[] Choices => choices;
    
}

[Serializable]
public class Choice
{
    [SerializeField]
    private string choiceText;
    
    public string ChoiceText => choiceText;

    [SerializeField]
    private ActionType action;

    public ActionType Action => action;

    public enum ActionType
    {
        Generic = 0,
        BuyDrugs = 1,
        SellDrugs = 2,
        BuyWeapon = 3,
        Steal = 4,
        Rape = 5,
        BuyDrink = 6,
        GetRobbed = 7,
        GoBrothel = 8,
        GetOral = 9,
        GetAnal = 10,
        GetVaginal = 11,
        GameOver = 12,
        BuyParadise = 13,
        BuyRaspadinha = 14
    }

    [SerializeField]
    private Dialogue successDialogue01;

    public Dialogue SuccessDialogue01 => successDialogue01;
    
    [SerializeField]
    private Dialogue successDialogue02;

    public Dialogue SuccessDialogue02 => successDialogue02;

    [SerializeField]
    private Dialogue failedDialogue;

    public Dialogue FailedDialogue => failedDialogue;

    [SerializeField]
    private Dialogue failedNoDrugsDialogue;
    public Dialogue FailedNoDrugsDialogue => failedNoDrugsDialogue;
    
    [SerializeField]
    private Dialogue failedNoMoneyDialogue;
    public Dialogue FailedNoMoneyDialogue => failedNoMoneyDialogue;
    
    [SerializeField]
    private Dialogue failedNoWeaponDialogue;
    public Dialogue FailedNoWeaponDialogue => failedNoWeaponDialogue;

    [SerializeField]
    private Dialogue nextDialogue;
    
    public Dialogue NextDialogue => nextDialogue;

    [SerializeField]
    public UnityEvent triggerEvent;
    
}




