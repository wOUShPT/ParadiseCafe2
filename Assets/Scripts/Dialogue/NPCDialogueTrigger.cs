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
    public Transform sphereOverlapOrigin;
    public float sphereOverlapRadius;
    public DialogueTriggerEvent triggerDialogue;
    private Transform playerTransform;
    private FMOD.Studio.EventInstance _dialogueSignStinger;
    private bool _canShowPrompt;
    
    
    private void Awake()
    {
        _npcDialogueReferences = GetComponent<NPCDialogueReferences>();
        _npcDialogueReferences.dialoguePrompt.SetActive(false);
        _canShowPrompt = true;
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

    public void Update()
    {
        Collider[] hitList = Physics.OverlapSphere(sphereOverlapOrigin.position, sphereOverlapRadius, 1 << 6);

        if (hitList.Length != 0)
        {
            Vector2 npcToPlayer2DVector = new Vector2(playerTransform.position.x, playerTransform.position.z) - new Vector2(sphereOverlapOrigin.position.x, sphereOverlapOrigin.position.z);
            Vector2 npcForward2DVector = new Vector2(sphereOverlapOrigin.forward.x, sphereOverlapOrigin.forward.z);
            if (Vector2.Angle(npcForward2DVector, npcToPlayer2DVector) < 70)
            {
                if (_canShowPrompt)
                {
                    _npcDialogueReferences.dialoguePrompt.SetActive(true);
                    _canShowPrompt = false;
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
        }

        _canShowPrompt = true;
        _npcDialogueReferences.dialoguePrompt.SetActive(false);
    }
}


public class DialogueTriggerEvent : UnityEvent<NPCDialogueReferences>
{
    
}
