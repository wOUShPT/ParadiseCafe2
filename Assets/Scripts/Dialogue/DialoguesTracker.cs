using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class DialoguesTracker : MonoBehaviour
{
    protected int numberOfOccurredInteractions;
    protected NPCInteractionsTracker _npcInteractionsTracker;
    public Dialogue firstInteractionDayDialogue;
    public Dialogue firstInteractionNightDialogue;
    public Dialogue dayDialogue;
    public Dialogue nightDialogue;
    private TimeController _timeController;
    protected DialogueTrigger _dialogueTrigger;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        numberOfOccurredInteractions = 0;
        _dialogueTrigger = GetComponent<DialogueTrigger>();
        _npcInteractionsTracker = FindObjectOfType<NPCInteractionsTracker>();
        _dialogueTrigger.triggerDialogue.AddListener(UpdateNumberOfInteractions);
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(UpdateDialogue);
        GetNumberOfInteractions();
        UpdateDialogue();
        
    }

    protected virtual void UpdateDialogue()
    {
        GetNumberOfInteractions();
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            if (numberOfOccurredInteractions == 0)
            {
                _dialogueTrigger.dialogue = firstInteractionDayDialogue;
                return;
            }
            
            _dialogueTrigger.dialogue = dayDialogue;
            return;
        }

        if (_timeController.dayState == TimeController.DayState.Night)
        {
            if (numberOfOccurredInteractions == 0)
            {
                _dialogueTrigger.dialogue = firstInteractionNightDialogue;
                return;
            }
            
            _dialogueTrigger.dialogue = nightDialogue;
        }
    }

    protected virtual void GetNumberOfInteractions()
    {
        switch (_dialogueTrigger.npcStats.iD)
        {
            case "Bofia":

                numberOfOccurredInteractions = _npcInteractionsTracker.Bofia.numberOfInteractions;
                break;
            
            case "Velha":

                numberOfOccurredInteractions = _npcInteractionsTracker.Velha.numberOfInteractions;
                break;
            
            case "Guna":

                numberOfOccurredInteractions = _npcInteractionsTracker.Guna.numberOfInteractions;
                break;
            
            case "Adriano":

                numberOfOccurredInteractions = _npcInteractionsTracker.Adriano.numberOfInteractions;
                break;
            
            case "SenhorTono":

                numberOfOccurredInteractions = _npcInteractionsTracker.SrTono.numberOfInteractions;
                break;
            
            case "Dealer":

                numberOfOccurredInteractions = _npcInteractionsTracker.Dealer.numberOfInteractions;
                break;
            
            case "Dom":

                numberOfOccurredInteractions = _npcInteractionsTracker.Dom.numberOfInteractions;
                break;
        }
    }
    
    
    protected virtual void UpdateNumberOfInteractions(DialogueTrigger dialogueTrigger)
    {
        switch (dialogueTrigger.npcStats.iD)
        {
            case "Bofia":

                _npcInteractionsTracker.Bofia.numberOfInteractions++;
                break;
            
            case "Velha":

                _npcInteractionsTracker.Velha.numberOfInteractions++;
                break;
            
            case "Guna":

                _npcInteractionsTracker.Guna.numberOfInteractions++;
                break;
            
            case "Adriano":

                _npcInteractionsTracker.Adriano.numberOfInteractions++;
                break;
            
            case "SenhorTono":

                _npcInteractionsTracker.SrTono.numberOfInteractions++;
                break;
            
            case "Dealer":

                _npcInteractionsTracker.Dealer.numberOfInteractions++;
                break;
            
            case "Dom":

                _npcInteractionsTracker.Dom.numberOfInteractions++;
                break;
        }
        UpdateDialogue();
    }
}