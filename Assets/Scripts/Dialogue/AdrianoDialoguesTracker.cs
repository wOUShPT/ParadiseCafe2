using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AdrianoDialoguesTracker : DialoguesTracker
{
    public Dialogue firstInteractionDialogue;
    public List<Dialogue> dialogues;

    protected override IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        NpcDialogueReferences = GetComponent<NPCDialogueReferences>();
        GetComponent<NPCDialogueTrigger>().triggerDialogue.AddListener((NpcDialogueReferences) => StartCoroutine(UpdateNumberOfInteractions(base.NpcDialogueReferences)));
        UpdateDialogue();
    }

    protected override void UpdateDialogue()
    {
        if (NpcDialogueReferences.npcStats.NumberOfInteractionsMade == 0)
        {
            NpcDialogueReferences.dialogue = firstInteractionDialogue;
            return;
        }
        NpcDialogueReferences.dialogue = dialogues[Random.Range(0, dialogues.Count - 1)];
    }
}
