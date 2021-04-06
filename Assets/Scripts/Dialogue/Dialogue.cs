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
    private Choice[] choices;

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
        BuyPistol = 3,
        Steal = 4,
        Rape = 5
    }

    [SerializeField]
    private Dialogue successDialogue;
    
    public Dialogue SuccessDialogue => successDialogue;

    [SerializeField]
    private Dialogue failedDialogue;

    public Dialogue FailedDialogue => failedDialogue;

    [SerializeField]
    private Dialogue nextDialogue;
    
    public Dialogue NextDialogue => nextDialogue;

    [SerializeField]
    public UnityEvent triggerEvent;
    
}




