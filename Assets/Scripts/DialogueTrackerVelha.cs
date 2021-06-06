using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrackerVelha : DialoguesTracker
{
    private int numberOfRapeInteractions;
    private int numberOfStealInteractions;

    protected override void UpdateDialogue()
    {
        GetNumberOfInteractions();
        if (numberOfOccurredInteractions == 0)
        {
            NpcDialogueReferences.dialogue = firstInteractionDayDialogue;
            return;
        }
        
        NpcDialogueReferences.dialogue = dayDialogue;
    }

    protected override void GetNumberOfInteractions()
    {
        switch (NpcDialogueReferences.npcStats.iD)
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

    protected override void UpdateNumberOfInteractions(NPCDialogueReferences npcDialogueReferences)
    {
        switch (npcDialogueReferences.npcStats.iD)
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

    public void UpdateNumberOfRapeInteractions()
    {
        _npcInteractionsTracker.Velha.numberOfRapeInteractions++;
    }

    public void UpdateNumberOfStealInteractions()
    {
        _npcInteractionsTracker.Velha.numberOfStealInteractions++;
    }
}
