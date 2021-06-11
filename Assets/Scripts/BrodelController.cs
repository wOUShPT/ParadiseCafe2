using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BrodelController : MonoBehaviour
{
    public PlayerStats _playerStats;
    private DialogueManager _dialogueManager;
    private LevelManager _levelManager;
    public Animator sexAnimator;
    public NPCDialogueReferences npcDialogueReferences;
    public SexDialogueTrigger sexDialogueTrigger;
    public Dialogue brodelMoneyBranch;
    public Dialogue brodelNoMoneyBranch;
    private Transform _charactersTransform;
    public Transform idlePivot;
    public Transform oralPivot;
    public Transform vaginalPivot;
    public Transform analPivot;
    public Transform payPivot;
    private Animator _sceneTransitionAnimator;
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _charactersTransform = sexAnimator.transform;
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        GameActions gameActions = FindObjectOfType<GameActions>();
        gameActions.Brodel.AddListener(Initialize);
        gameActions.OralSex.AddListener(StartOral);
        gameActions.VaginalSex.AddListener(StartVaginal);
        gameActions.AnalSex.AddListener(StartAnal);
    }

    void Initialize(NPCStats npcStats)
    {
        _charactersTransform.position = idlePivot.position;
    }

    void StartOral(NPCStats npcStats)
    {
        StartCoroutine(OralSex());
    }

    void StartVaginal(NPCStats npcStats)
    {
        StartCoroutine(VaginalSex());
    }

    void StartAnal(NPCStats npcStats)
    {
        StartCoroutine(AnalSex());
    }

    IEnumerator OralSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = oralPivot.position;
        sexAnimator.SetTrigger("TriggerOral");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(16f);
        StartCoroutine(Pay());
        
    }

    IEnumerator VaginalSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = vaginalPivot.position;
        sexAnimator.SetTrigger("TriggerVaginal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(16f);
        StartCoroutine(Pay());
    }

    IEnumerator AnalSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = analPivot.position;
        sexAnimator.SetTrigger("TriggerAnal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(16f);
        StartCoroutine(Pay());
    }

    IEnumerator Pay()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = payPivot.position;
        if (_playerStats.moneyAmount < 100)
        {
            npcDialogueReferences.dialogue = brodelNoMoneyBranch;
            sexAnimator.SetTrigger("PayNoMoney");
            sexDialogueTrigger.TriggerDialogue();
            _dialogueManager.endedDialogue.AddListener(() => StartCoroutine(Reginaldo()));
        }
        else
        {
            npcDialogueReferences.dialogue = brodelMoneyBranch;
            _playerStats.moneyAmount -= 100;
            sexAnimator.SetTrigger("PayMoney");
            sexDialogueTrigger.TriggerDialogue();
            
        }
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
    }

    IEnumerator Reginaldo()
    {
        sexAnimator.SetTrigger("Reginaldo");
        yield return new WaitForSeconds(20f);
        
    }

}
