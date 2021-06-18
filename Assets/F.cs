using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F : MonoBehaviour
{
    private InputManager _inputManager;
    private DialogueManager _dialogueManager;
    public BoxCollider barrier;
    private NPCDialogueReferences _npcDialogueReferences;
    void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _npcDialogueReferences = GetComponent<NPCDialogueReferences>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _inputManager.ActionInput == 1)
        {
            barrier.enabled = false;
            _dialogueManager.StartDialogue(_npcDialogueReferences);
            gameObject.SetActive(false);
        }
    }
}
