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
    public DialogueActions dialogueActions;
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
        dialogueTriggers = FindObjectsOfType<DialogueTrigger>().ToList();
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
        _playerController.enabled = false;
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
        endedDialogue.RemoveAllListeners();
        _playerController.enabled = true;
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
            dialogueChoiceSelection.enabled = true;
        }
        
        while (waiting)
        {
            if (_currentDialogue.Type == Dialogue.TypeProp.Branch && _choiceSelected)
            {
                yield return new WaitForSeconds(0.05f);
                for (int i = 0; i < dialogueChoiceSelection.buttons.Count; i++)
                {
                    dialogueChoiceSelection.buttons[i].text = "";
                    dialogueChoiceSelection.buttons[i].gameObject.SetActive(false);
                    dialogueChoiceSelection.enabled = false;
                }

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
                if (_currentDialogue.Choices[index].triggerEvent != null)
                {
                    dialogueActions.Generic.Invoke(playerStats, _currentNpcStats);
                }
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                
                break;
            
            case Choice.ActionType.Rape:
                if (playerStats.hasWeapon && !_currentNpcStats.hasWeapon)
                {
                    dialogueActions.Rape.Invoke(playerStats, _currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                }
                else
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                }
                
                break;
            
            case Choice.ActionType.Steal:
                if (playerStats.hasWeapon && !_currentNpcStats.hasWeapon)
                {
                    dialogueActions.Steal.Invoke(playerStats, _currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                }
                else
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                }
                break;
            
            case Choice.ActionType.BuyDrugs:
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buy)
                {
                    dialogueActions.Buy.Invoke(playerStats, _currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                }
                else
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;

                }
                break;
            
            case Choice.ActionType.SellDrugs:
                if (_currentNpcStats.moneyAmount >= _currentNpcStats.tradePrices.sell && playerStats.drugsAmount != 0)
                {
                    dialogueActions.Sell.Invoke(playerStats, _currentNpcStats);
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue;
                }
                else
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                }
                break;
        }
    }
}
