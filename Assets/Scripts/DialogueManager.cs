using System;
using System.Collections;
using System.Collections.Generic;
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
    public DialogueEvents dialogueEvents;
    private int _choiceSelectedIndex;
    private bool _choiceSelected;
    private InputManager _InputManager;
    private ThirdPersonController _playerController;
    private DialogueTrigger _currentDialogueTrigger;
    private Dialogue _currentDialogue;

    private void Awake()
    {
        _playerController = FindObjectOfType<ThirdPersonController>();
        _InputManager = FindObjectOfType<InputManager>();
        _currentDialogueTrigger = null;
        _choiceSelected = false;
        _choiceSelectedIndex = 0;
    }

    private void Start()
    {
        dialogueChoiceSelection.ButtonSelect.AddListener(SelectChoice);
        dialogueChoiceSelection.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger npcDialogueTrigger)
    {
        _playerController.enabled = false;
        _currentDialogueTrigger = npcDialogueTrigger;
        _currentDialogue = dialogue;
        dialogueBoxAnimator.SetBool("IsOpen", true);
        Debug.Log("StartingConversation with " + _currentDialogue.characterName);
        nameText.text = _currentDialogue.characterName;
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

        StartCoroutine(TypeSentence(_currentDialogue.sentence));
        Debug.Log(_currentDialogue.sentence);
    }

    public void EndDialogue()
    {
        _playerController.enabled = true;
        StartCoroutine(IdleDialogueTrigger());
        dialogueBoxAnimator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (var letter in _currentDialogue.sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (_currentDialogue.hasChoices)
        {
            for (int i = 0; i < _currentDialogue.choices.Count; i++)
            {
                dialogueChoiceSelection.buttons[i].text = _currentDialogue.choices[i].choiceText;
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
        if (_currentDialogue.hasChoices)
        {
            dialogueChoiceSelection.enabled = true;
        }
        
        while (waiting)
        {
            if (_currentDialogue.hasChoices && _choiceSelected)
            {
                yield return new WaitForSeconds(0.05f);
                for (int i = 0; i < dialogueChoiceSelection.buttons.Count; i++)
                {
                    dialogueChoiceSelection.buttons[i].text = "";
                    dialogueChoiceSelection.buttons[i].gameObject.SetActive(false);
                    dialogueChoiceSelection.enabled = false;
                    dialogueChoiceSelection.ButtonSelect.RemoveAllListeners();
                }

                _currentDialogue = _currentDialogue.choices[_choiceSelectedIndex].nextDialogue;
                _choiceSelectedIndex = 0;
                _choiceSelected = false;
                waiting = false; 
                DisplayNextSentence();
            }
            else if (!_currentDialogue.hasChoices && _InputManager.ConfirmButton == 1)
            {
                _currentDialogue = _currentDialogue.nextDialogue;
                waiting = false; 
                DisplayNextSentence();
            }
            yield return null;
        }
    }

    IEnumerator IdleDialogueTrigger()
    {
        yield return new WaitForSeconds(2);
        _currentDialogueTrigger.enabled = true;
    }

    IEnumerator DelayNextDialogue(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        DisplayNextSentence();
    }

    void SelectChoice(int index)
    {
        CallAction(_currentDialogue.choices[index].action, index);
        _choiceSelectedIndex = index;
        _choiceSelected = true;
    }

    
    void CallAction(Choice.Action action, int buttonIndex)
    {
        switch (action)
        {
            case Choice.Action.Rape:
                dialogueEvents.DoBuy();
                break;
            
            case Choice.Action.Steal:
                dialogueEvents.DoSteal();
                break;
            
            case Choice.Action.BuyDrugs:
                dialogueEvents.DoBuy();
                break;
            
            case Choice.Action.SellDrugs:
                dialogueEvents.DoSell();
                break;
        }
    }
}
