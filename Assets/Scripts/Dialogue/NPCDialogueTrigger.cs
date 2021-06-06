using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NPCDialogueReferences))]
public class NPCDialogueTrigger : MonoBehaviour
{
    private NPCDialogueReferences _npcDialogueReferences;
    private DialogueManager _dialogueManager;
    private InputManager _inputManager;
    public LayerMask layerMask;
    public string playerTag;
    public Transform sphereCastOrigin;
    public float dialogueMinDistance;
    public float dialogueMaxDistance;
    public DialogueTriggerEvent triggerDialogue;
    public float radius;
    private Transform playerTransform;
    private FMOD.Studio.EventInstance _dialogueSignStinger;
    private bool canShowPrompt;
    
    
    private void Awake()
    {
        _npcDialogueReferences = GetComponent<NPCDialogueReferences>();
        _npcDialogueReferences.dialoguePrompt.SetActive(false);
        canShowPrompt = true;
        _dialogueSignStinger = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/OpçãoInteração");
        _inputManager = FindObjectOfType<InputManager>();
        triggerDialogue = new DialogueTriggerEvent();
        triggerDialogue.AddListener(FindObjectOfType<DialogueManager>().StartDialogue);
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void TriggerDialogue()
    {
        triggerDialogue.Invoke( _npcDialogueReferences);
    }

    private void OnEnable()
    {
        triggerDialogue.AddListener(FindObjectOfType<DialogueManager>().StartDialogue);
        _npcDialogueReferences.dialoguePrompt.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(sphereCastOrigin.position, radius, transform.forward, out hit, dialogueMaxDistance+2, layerMask) && hit.transform.CompareTag(playerTag) && (hit.distance < dialogueMaxDistance && hit.distance > dialogueMinDistance))
        {
            if (canShowPrompt)
            {
                _npcDialogueReferences.dialoguePrompt.SetActive(true);
                canShowPrompt = false;
                _dialogueSignStinger.start();
            }
            
            if (_inputManager.ActionInput == 1)
            {
                _npcDialogueReferences.dialoguePrompt.SetActive(false);
                playerTransform.LookAt(transform.position);
                playerTransform.rotation = Quaternion.Euler(0, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
                TriggerDialogue();
                enabled = false;
            }
            return;
        }

        canShowPrompt = true;
        _npcDialogueReferences.dialoguePrompt.SetActive(false);
    }
}


public class DialogueTriggerEvent : UnityEvent<NPCDialogueReferences>
{
    
}
