using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueReferences : MonoBehaviour
{
    public GameObject dialoguePrompt;
    public Dialogue dialogue;
    public NPCStats npcStats;
    public string dialogueSoundFMODEventRoot;
    public FMOD.Studio.EventInstance dialogueSound;

    private void Start()
    {
        if (dialogueSoundFMODEventRoot != "")
        {
            dialogueSound = FMODUnity.RuntimeManager.CreateInstance(dialogueSoundFMODEventRoot);
            dialogueSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.parent));
        }
    }
}
