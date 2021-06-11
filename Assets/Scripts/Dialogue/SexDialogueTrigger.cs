using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADBannerView = UnityEngine.iOS.ADBannerView;

[RequireComponent(typeof(NPCDialogueReferences))]
public class SexDialogueTrigger : MonoBehaviour
{
    private NPCDialogueReferences _npcDialogueReferences;
    private DialogueManager _dialogueManager;
    public DialogueTriggerEvent triggerDialogue;
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _npcDialogueReferences = GetComponent<NPCDialogueReferences>();
        triggerDialogue = new DialogueTriggerEvent();
        triggerDialogue.AddListener(FindObjectOfType<DialogueManager>().StartDialogue);
        enabled = false;
    }
    

    public void TriggerDialogue()
    {
        triggerDialogue.Invoke(_npcDialogueReferences);
    }
}
