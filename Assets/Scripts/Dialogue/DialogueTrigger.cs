using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private InputManager _inputManager;
    public GameObject dialoguePrompt;
    public LayerMask layerMask;
    public string playerTag;
    public Dialogue dialogue;
    public NPCStats npcStats;
    public Transform sphereCastOrigin;
    public float dialogueMinDistance;
    public float dialogueMaxDistance;
    public DialogueTriggerEvent triggerDialogue;
    public float radius;
    private Transform playerTransform;
    

    private void Awake()
    {
        dialoguePrompt.SetActive(false);
        _inputManager = FindObjectOfType<InputManager>();
        triggerDialogue = new DialogueTriggerEvent();
        triggerDialogue.AddListener(FindObjectOfType<DialogueManager>().StartDialogue);
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void TriggerDialogue()
    {
        triggerDialogue.Invoke( this);
    }

    private void OnEnable()
    {
        triggerDialogue.AddListener(FindObjectOfType<DialogueManager>().StartDialogue);
        dialoguePrompt.SetActive(false);
    }

    private void Update()
    {
        dialoguePrompt.SetActive(false);
        RaycastHit hit;
        if (Physics.SphereCast(sphereCastOrigin.position, radius, transform.forward, out hit, dialogueMaxDistance+2, layerMask) && hit.transform.CompareTag(playerTag) && (hit.distance < dialogueMaxDistance && hit.distance > dialogueMinDistance))
        {
            dialoguePrompt.SetActive(true);
            if (_inputManager.ActionInput == 1)
            {
                dialoguePrompt.SetActive(false);
                playerTransform.LookAt(transform.position);
                playerTransform.rotation = Quaternion.Euler(0, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
                TriggerDialogue();
                enabled = false;
            }
        }
    }
}

public class DialogueTriggerEvent : UnityEvent<DialogueTrigger>
{
    
}
