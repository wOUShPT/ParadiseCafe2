using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private InputManager _inputManager;
    public GameObject dialoguePrompt;
    public string playerTag;
    public Dialogue dialogue;
    public float dialogueMinDistance;
    public float dialogueMaxDistance;
    private Transform playerTransform;
    

    private void Awake()
    {
        dialoguePrompt.SetActive(false);
        _inputManager = FindObjectOfType<InputManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void TriggerDialogue()
    {
        _dialogueManager.StartDialogue(dialogue, this);
    }
    
    private void Update()
    {
        dialoguePrompt.SetActive(false);
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 2, transform.forward, out hit, dialogueMaxDistance+2) && hit.transform.CompareTag(playerTag) && (hit.distance < dialogueMaxDistance && hit.distance > dialogueMinDistance))
        {
            dialoguePrompt.SetActive(true);
            if (_inputManager.ActionInput == 1)
            {
                dialoguePrompt.SetActive(false);
                playerTransform.LookAt(transform.position);
                TriggerDialogue();
                enabled = false;
            }
        }
    }
}
