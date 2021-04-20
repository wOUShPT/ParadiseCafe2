using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public DialogueChoiceSelection dialogueChoiceSelection;
    private List<TextMeshProUGUI> _optionButtonsText;
    public Animator dialogueBoxAnimator;
    public float typingSpeed;
    public GameActions gameActions;
    public PlayerStats playerStats;
    private int _choiceSelectedIndex;
    private bool _choiceSelected;
    private InputManager _InputManager;
    private ThirdPersonController _playerController;
    private List<DialogueTrigger> dialogueTriggers;
    private DialogueTrigger _currentDialogueTrigger;
    private NPCStats _currentNpcStats;
    private Dialogue _currentDialogue;
    private Dialogue _nextDialogue;
    public UnityEvent startedDialogue;
    public UnityEvent endedDialogue;

    private void Awake()
    {
        startedDialogue = new UnityEvent();
        endedDialogue = new UnityEvent();
        _playerController = FindObjectOfType<ThirdPersonController>();
        _InputManager = FindObjectOfType<InputManager>();
        _currentNpcStats = null;
        _choiceSelected = false;
        _choiceSelectedIndex = 0;
    }

    private void Start()
    {
        dialogueChoiceSelection.ButtonSelect.AddListener(SelectChoice);
        dialogueChoiceSelection.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger npcDialogueTrigger, NPCStats npcStats)
    {
        startedDialogue.Invoke();
        dialogueTriggers = null;
        dialogueTriggers = FindObjectsOfType<DialogueTrigger>().ToList();
        foreach (var dialogueTrigger in dialogueTriggers)
        {
            dialogueTrigger.enabled = false;
        }
        _currentNpcStats = npcStats;
        _currentDialogue = dialogue;
        dialogueBoxAnimator.SetBool("IsOpen", true);
        Debug.Log("StartingConversation with " + _currentDialogue.CharacterName);
        nameText.text = _currentDialogue.CharacterName;
        StartCoroutine(DelayNextDialogue(1));
    }

    public void DisplayNextSentence()
    {
        if (_currentDialogue == null)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();

        nameText.text = _currentDialogue.CharacterName;
        StartCoroutine(TypeSentence(_currentDialogue.Sentence));
        Debug.Log(_currentDialogue.Sentence);
    }

    public void EndDialogue()
    {
        endedDialogue.Invoke();
        dialogueText.text = "";
        _choiceSelected = false;
        _choiceSelectedIndex = 0;
        StartCoroutine(DialogueTriggerWaitToEnable());
        dialogueBoxAnimator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (var letter in _currentDialogue.Sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (_currentDialogue.Type == Dialogue.TypeProp.Branch)
        {
            for (int i = 0; i < _currentDialogue.Choices.Length; i++)
            {
                dialogueChoiceSelection.buttons[i].text = _currentDialogue.Choices[i].ChoiceText;
                dialogueChoiceSelection.buttons[i].gameObject.SetActive(true);
            }
            _choiceSelected = false;
            _choiceSelectedIndex = 0;
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckDialogueNextInput());
    }

    IEnumerator CheckDialogueNextInput()
    {
        bool waiting = true;
        if (_currentDialogue.Type == Dialogue.TypeProp.Branch)
        {
            dialogueChoiceSelection.StartCoroutine(dialogueChoiceSelection.Selection());
        }
        
        while (waiting)
        {
            if (_currentDialogue.Type == Dialogue.TypeProp.Branch && _choiceSelected)
            {
                yield return new WaitForSeconds(0.05f);
                for (int i = 0; i < dialogueChoiceSelection.buttons.Count; i++)
                {
                    dialogueChoiceSelection.buttons[i].gameObject.SetActive(false);
                }
                dialogueChoiceSelection.ResetButtons();
                _currentDialogue = _nextDialogue;
                _nextDialogue = null;
                _choiceSelectedIndex = 0;
                _choiceSelected = false;
                waiting = false; 
                DisplayNextSentence();
            }
            else if (_currentDialogue.Type == Dialogue.TypeProp.Normal && _InputManager.ConfirmButton == 1)
            {
                _currentDialogue = _currentDialogue.NextDialogue;
                waiting = false; 
                DisplayNextSentence();
            }
            yield return null;
        }
    }

    IEnumerator DialogueTriggerWaitToEnable()
    {
        yield return new WaitForSeconds(2);
        foreach (var dialogueTrigger in dialogueTriggers)
        {
            dialogueTrigger.enabled = true;
        }
    }

    IEnumerator DelayNextDialogue(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        DisplayNextSentence();
    }

    void SelectChoice(int index)
    {
        CallAction(_currentDialogue.Choices[index].Action, index);
        _choiceSelectedIndex = index;
        _choiceSelected = true;
    }

    
    void CallAction(Choice.ActionType action, int index)
    {
        switch (action)
        {
            case Choice.ActionType.Generic:
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                break;
            
            case Choice.ActionType.Rape:
                if (playerStats.hasWeapon && !_currentNpcStats.hasWeapon)
                {
                    gameActions.Rape.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }
                
                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
            
            case Choice.ActionType.Steal:
                if (playerStats.hasWeapon && !_currentNpcStats.hasWeapon)
                {
                    gameActions.Steal.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }

                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
            
            case Choice.ActionType.BuyDrugs:
                if (playerStats.moneyAmount == 0)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoDrugsDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buy)
                {
                    gameActions.BuyDrugs.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }
                
                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
            
            case Choice.ActionType.BuyPistol:
                if (playerStats.moneyAmount == 0)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoMoneyDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buyPistol)
                {
                    gameActions.BuyPistol.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }
                
                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
            
            case Choice.ActionType.BuyDrink:
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buy)
                {
                    gameActions.BuyDrink.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }
                
                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
            
            case Choice.ActionType.SellDrugs:
                if (playerStats.drugsAmount == 0)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoDrugsDialogue;
                    return;
                }
                
                if (_currentNpcStats.iD == "Bofia" && _currentNpcStats.aggressiveness >= 3)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                    return;
                }

                if (_currentNpcStats.moneyAmount > _currentNpcStats.tradePrices.sell)
                {
                    gameActions.SellDrugs.Invoke(_currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                    return;
                }
                
                _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                
                break;
        }
    }
}
