using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public DialogueChoiceSelection dialogueChoiceSelection;
    private List<TextMeshProUGUI> _optionButtonsText;
    public Animator dialogueBoxAnimator;
    public GameObject nextDialogueArrowIndicator;
    public float typingSpeed;
    public GameActions gameActions;
    public PlayerStats playerStats;
    private int _choiceSelectedIndex;
    private bool _choiceSelected;
    private InputManager _InputManager;
    private ThirdPersonController _playerController;
    private List<NPCDialogueTrigger> dialogueTriggers;
    private NPCDialogueTrigger _currentNpcDialogueTrigger;
    private NPCInteractionsTracker npcInteractionsTracker;
    private GameObject _currentNpc;
    private NPCStats _currentNpcStats;
    private Dialogue _currentDialogue;
    private Dialogue _nextDialogue;
    public UnityEvent startedDialogue;
    public UnityEvent endedDialogue;
    public GameObject CurrentNPC => _currentNpc;

    private void Awake()
    {
        startedDialogue = new UnityEvent();
        endedDialogue = new UnityEvent();
        _InputManager = FindObjectOfType<InputManager>();
        npcInteractionsTracker = FindObjectOfType<NPCInteractionsTracker>();
        _currentNpcStats = null;
        _choiceSelected = false;
        _choiceSelectedIndex = 0;
    }

    private void Start()
    {
        dialogueChoiceSelection.ButtonSelect.AddListener(SelectChoice);
        dialogueChoiceSelection.enabled = false;
    }

    public void StartDialogue(NPCDialogueReferences npcDialogueReferences)
    {
        _currentNpc = npcDialogueReferences.GetComponentInParent<Transform>().parent.gameObject;
        _playerController = FindObjectOfType<ThirdPersonController>();
        startedDialogue.AddListener(() => { _playerController.FreezePlayer(true); });
        endedDialogue.AddListener(() => { _playerController.FreezePlayer(false); });
        NpcAIBehaviour _currentNPCAgent = _currentNpc.GetComponent<NpcAIBehaviour>();
        if (_currentNPCAgent != null)
        {
            startedDialogue.AddListener(_currentNPCAgent.StopMovement);
            endedDialogue.AddListener(_currentNPCAgent.ResumeMovement);
        }
        NpcAnimationController _currentNPCAnimationController = _currentNpc.GetComponentInChildren<NpcAnimationController>();
        if (_currentNPCAnimationController != null)
        {
            startedDialogue.AddListener(_currentNPCAnimationController.EnterInteraction);
            endedDialogue.AddListener(_currentNPCAnimationController.ExitInteraction);
        }
        startedDialogue.Invoke();
        dialogueTriggers = FindObjectsOfType<NPCDialogueTrigger>().ToList();
        foreach (var dialogueTrigger in dialogueTriggers)
        {
            dialogueTrigger.enabled = false;
        }
        _currentNpcStats = npcDialogueReferences.npcStats;
        _currentDialogue = npcDialogueReferences.dialogue;
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

        if (_currentDialogue.Type == Dialogue.TypeProp.Normal)
        {
            nextDialogueArrowIndicator.SetActive(true);
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
                nextDialogueArrowIndicator.SetActive(false);
                DisplayNextSentence();
            }
            yield return null;
        }
    }

    IEnumerator DialogueTriggerWaitToEnable()
    {
        yield return new WaitForSeconds(2);
        startedDialogue.RemoveAllListeners();
        endedDialogue.RemoveAllListeners();
        foreach (var dialogueTrigger in dialogueTriggers)
        {
            if (dialogueTrigger != null)
            {
                dialogueTrigger.enabled = true;
            }
        }
        dialogueTriggers = null;
    }

    IEnumerator DelayNextDialogue(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        DisplayNextSentence();
    }

    void SelectChoice(int index)
    {
        AssignAction(_currentDialogue.Choices[index].Action, index);
        _choiceSelectedIndex = index;
        _choiceSelected = true;
    }

    
    void AssignAction(Choice.ActionType action, int index)
    {
        switch (action)
        {
            case Choice.ActionType.Generic:
                
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.Generic.Invoke(_currentNpcStats));
                
                break;
            
            case Choice.ActionType.Rape:
                
                if (!playerStats.hasWeapon)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoWeaponDialogue;
                    return;
                }
                
                if (playerStats.hasWeapon)
                {

                    if (playerStats.wantedLevel == 3)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                        endedDialogue.AddListener(() => gameActions.GetBustedVelha.Invoke(_currentNpcStats));
                        return;
                    }
                    
                    if (npcInteractionsTracker.Velha.numberOfRapeInteractions == 0)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
                        endedDialogue.AddListener(() => gameActions.Rape.Invoke(_currentNpcStats));
                        PlayerAnimationController playerAnimationController =
                            FindObjectOfType<PlayerAnimationController>();
                        playerAnimationController.PointWeapon();
                        endedDialogue.AddListener(playerAnimationController.HolsterWeapon);
                        return;
                    }

                    if (npcInteractionsTracker.Velha.numberOfRapeInteractions >= 1)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue02;
                        endedDialogue.AddListener(() => gameActions.Rape.Invoke(_currentNpcStats));
                        PlayerAnimationController playerAnimationController =
                            FindObjectOfType<PlayerAnimationController>();
                        playerAnimationController.PointWeapon();
                        endedDialogue.AddListener(playerAnimationController.HolsterWeapon);
                    }
                }

                break;
            
            case Choice.ActionType.Steal:
                
                if (!playerStats.hasWeapon)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoWeaponDialogue;
                    return;
                }
                
                if (playerStats.hasWeapon)
                {

                    if (playerStats.wantedLevel == 3)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                        endedDialogue.AddListener(() => gameActions.GetBustedVelha.Invoke(_currentNpcStats));
                        return;
                    }
                    
                    if (npcInteractionsTracker.Velha.numberOfStealInteractions == 0)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
                        endedDialogue.AddListener(() => gameActions.Steal.Invoke(_currentNpcStats));
                        PlayerAnimationController playerAnimationController =
                            FindObjectOfType<PlayerAnimationController>();
                        playerAnimationController.PointWeapon();
                        endedDialogue.AddListener(playerAnimationController.HolsterWeapon);
                        return;
                    }

                    if (npcInteractionsTracker.Velha.numberOfStealInteractions >= 1)
                    {
                        _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue02;
                        endedDialogue.AddListener(() => gameActions.Steal.Invoke(_currentNpcStats));
                        PlayerAnimationController playerAnimationController =
                            FindObjectOfType<PlayerAnimationController>();
                        playerAnimationController.PointWeapon();
                        endedDialogue.AddListener(playerAnimationController.HolsterWeapon);
                    }
                    
                }
                
                break;
            
            case Choice.ActionType.BuyDrugs:
                
                if (playerStats.moneyAmount < _currentNpcStats.tradePrices.buy)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoMoneyDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buy)
                {
                    endedDialogue.AddListener(() => gameActions.BuyDrugs.Invoke(_currentNpcStats));
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
                }
                
                
                break;
            
            case Choice.ActionType.BuyWeapon:

                if (playerStats.hasWeapon)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount == 0)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoMoneyDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buyPistol)
                {
                    endedDialogue.AddListener(() => gameActions.BuyWeapon.Invoke(_currentNpcStats));
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
                }

                break;
            
            case Choice.ActionType.BuyParadise:
                
                
                if (playerStats.moneyAmount < 200)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoMoneyDialogue;
                    return;
                }
                
                if (playerStats.moneyAmount >= 200)
                {
                    endedDialogue.AddListener(() => gameActions.BuyParadise.Invoke(_currentNpcStats));
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
                }

                break;
            
            case Choice.ActionType.BuyDrink:
                
                if (playerStats.moneyAmount >= _currentNpcStats.tradePrices.buy)
                {
                    endedDialogue.AddListener(() => gameActions.BuyDrink.Invoke(_currentNpcStats));
                    _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;
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
                
                endedDialogue.AddListener(() => gameActions.SellDrugs.Invoke(_currentNpcStats));
                _nextDialogue = _currentDialogue.Choices[index].SuccessDialogue01;

                break;
            
            case Choice.ActionType.GetRobbed:
                
                if (playerStats.drugsAmount == 0)
                {
                    _nextDialogue = _currentDialogue.Choices[index].FailedNoDrugsDialogue;
                    endedDialogue.AddListener(() => gameActions.GetRobbed.Invoke(_currentNpcStats));
                    return;
                }

                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.GetRobbed.Invoke(_currentNpcStats));
                
                break;
            
            case Choice.ActionType.GoBrothel:
                
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.Brothel.Invoke(_currentNpcStats));
                
                break;
            
            case Choice.ActionType.GetOral:
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.OralSex.Invoke(_currentNpcStats));
                
                break;
            
            
            case Choice.ActionType.GetVaginal:
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.VaginalSex.Invoke(_currentNpcStats));
                
                break;

            case Choice.ActionType.GetAnal:
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.AnalSex.Invoke(_currentNpcStats));
                
                break;
            
            case Choice.ActionType.GameOver:
                _nextDialogue = _currentDialogue.Choices[index].NextDialogue;
                endedDialogue.AddListener(() => gameActions.GameOver.Invoke(_currentNpcStats));
                
                break;
            
        }
    }
}
