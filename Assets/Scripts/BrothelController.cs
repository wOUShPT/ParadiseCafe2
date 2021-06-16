using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FMOD.Studio;
using UnityEngine;

public class BrothelController : MonoBehaviour
{
    public PlayerStats _playerStats;
    private DialogueManager _dialogueManager;
    private LevelManager _levelManager;
    private InputManager _inputManager;
    public Animator sexAnimator;
    public NPCDialogueReferences npcDialogueReferences;
    public SexDialogueTrigger sexDialogueTrigger;
    public Dialogue firstDialogue;
    public Dialogue brothelMoneyBranch;
    public Dialogue brothelNoMoneyBranch;
    private Transform _charactersTransform;
    public Animator doorAnimator;
    public Transform idlePivot;
    public Transform oralPivot;
    public Transform vaginalPivot;
    public Transform analPivot;
    public Transform payPivot;
    private Animator _sceneTransitionAnimator;
    public BrothelSounds _BrothelSounds;
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _inputManager = FindObjectOfType<InputManager>();
        _charactersTransform = sexAnimator.transform;
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        GameActions gameActions = FindObjectOfType<GameActions>();
       
        gameActions.Brothel.AddListener(Initialize);
        gameActions.OralSex.AddListener(StartOral);
        gameActions.VaginalSex.AddListener(StartVaginal);
        gameActions.AnalSex.AddListener(StartAnal);
    }

    void Initialize(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        _charactersTransform.position = idlePivot.position;
    }

    void StartOral(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        StartCoroutine(OralSex());
    }

    void StartVaginal(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        StartCoroutine(VaginalSex());
    }

    void StartAnal(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        StartCoroutine(AnalSex());
    }

    IEnumerator OralSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = oralPivot.position;
        _BrothelSounds.StopSounds();
        sexAnimator.SetTrigger("TriggerOral");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay());
        
    }

    IEnumerator VaginalSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = vaginalPivot.position;
        _BrothelSounds.StopSounds();
        sexAnimator.SetTrigger("TriggerVaginal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay());
    }

    IEnumerator AnalSex()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = analPivot.position;
        _BrothelSounds.StopSounds();
        sexAnimator.SetTrigger("TriggerAnal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay());
    }

    IEnumerator Pay()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = payPivot.position;
        if (_playerStats.moneyAmount < 100)
        {
            npcDialogueReferences.dialogue = brothelNoMoneyBranch;
            sexAnimator.SetTrigger("PayNoMoney");
            sexDialogueTrigger.TriggerDialogue();
            _dialogueManager.endedDialogue.AddListener(() => StartCoroutine(Reginaldo()));
        }
        else
        {
            npcDialogueReferences.dialogue = brothelMoneyBranch;
            _playerStats.moneyAmount -= 100;
            sexAnimator.SetTrigger("PayMoney");
            sexDialogueTrigger.TriggerDialogue();
            _dialogueManager.endedDialogue.AddListener(() =>
            {
                _levelManager.LoadOutBrothel();
                StartCoroutine(ResetScene());
            });
        }
        _BrothelSounds.StopSounds();
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
    }

    IEnumerator Reginaldo()
    {
        //_inputManager.TogglePlayerControls(false);
        doorAnimator.SetTrigger("Open");
        sexAnimator.SetTrigger("CallReginaldo");
        while (sexAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Este_nao_quer_pagar")
        {
            yield return null;
        }

        yield return new WaitForSeconds(sexAnimator.GetCurrentAnimatorStateInfo(0).length *
                                            sexAnimator.GetCurrentAnimatorStateInfo(0).speed);
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        sexAnimator.SetTrigger("Reginaldo");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(16f);
        _levelManager.LoadOutBrothel();
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1f);
        sexAnimator.SetTrigger("Reset");
        doorAnimator.SetTrigger("Reset");
        npcDialogueReferences.dialogue = firstDialogue;
    }
}