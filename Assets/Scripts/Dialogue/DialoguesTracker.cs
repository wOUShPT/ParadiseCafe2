using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCDialogueTrigger))]
public class DialoguesTracker : MonoBehaviour
{
    public Dialogue firstInteractionDayDialogue;
    public Dialogue firstInteractionNightDialogue;
    public Dialogue dayDialogue;
    public Dialogue nightDialogue;
    protected TimeController _timeController;
    protected NPCDialogueReferences NpcDialogueReferences;

    protected virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        NpcDialogueReferences = GetComponent<NPCDialogueReferences>();
        GetComponent<NPCDialogueTrigger>().triggerDialogue.AddListener((NpcDialogueReferences) => StartCoroutine(UpdateNumberOfInteractions(NpcDialogueReferences)));
        _timeController = FindObjectOfType<TimeController>();
        _timeController.dayStateChange.AddListener(UpdateDialogue);
        UpdateDialogue();
        
    }

    protected virtual void UpdateDialogue()
    {
        if (_timeController.dayState == TimeController.DayState.Day)
        {
            if (NpcDialogueReferences.npcStats.NumberOfInteractionsMade == 0)
            {
                NpcDialogueReferences.dialogue = firstInteractionDayDialogue;
                return;
            }
            
            NpcDialogueReferences.dialogue = dayDialogue;
            return;
        }

        if (_timeController.dayState == TimeController.DayState.Night)
        {
            if (NpcDialogueReferences.npcStats.NumberOfInteractionsMade == 0)
            {
                NpcDialogueReferences.dialogue = firstInteractionNightDialogue;
                return;
            }
            
            NpcDialogueReferences.dialogue = nightDialogue;
        }
    }
    
    
    protected IEnumerator UpdateNumberOfInteractions(NPCDialogueReferences npcDialogueReferences)
    {
        yield return new WaitForSeconds(0.5f);
        npcDialogueReferences.npcStats.NumberOfInteractionsMade++;
        UpdateDialogue();
    }
}
