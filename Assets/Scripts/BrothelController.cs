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
    public Dialogue brothelMoneyOralBranch;
    public Dialogue brothelMoneyVaginalBranch;
    public Dialogue brothelMoneyAnalBranch;
    public Dialogue brothelNoMoneyOralBranch;
    public Dialogue brothelNoMoneyVaginalBranch;
    public Dialogue brothelNoMoneyAnalBranch;
    private Transform _charactersTransform;
    public Animator doorAnimator;
    public Transform idlePivot;
    public Transform oralPivot;
    public Transform vaginalPivot;
    public Transform analPivot;
    public Transform payPivot;
    private Animator _sceneTransitionAnimator;
    public BrothelSounds _brothelSounds;
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
        StartCoroutine(Oral());
    }

    void StartVaginal(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        StartCoroutine(Vaginal());
    }

    void StartAnal(NPCStats npcStats)
    {
        _inputManager.TogglePlayerControls(false);
        StartCoroutine(Anal());
    }

    IEnumerator Oral()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = oralPivot.position;
        _brothelSounds.StopSounds(STOP_MODE.IMMEDIATE);
        sexAnimator.SetTrigger("TriggerOral");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay(1));
        
    }

    IEnumerator Vaginal()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = vaginalPivot.position;
        _brothelSounds.StopSounds(STOP_MODE.IMMEDIATE);
        sexAnimator.SetTrigger("TriggerVaginal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay(2));
    }

    IEnumerator Anal()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = analPivot.position;
        _brothelSounds.StopSounds(STOP_MODE.IMMEDIATE);
        sexAnimator.SetTrigger("TriggerAnal");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(15f);
        StartCoroutine(Pay(3));
    }

    IEnumerator Pay(int sexType)
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _charactersTransform.position = payPivot.position;
        switch (sexType)
        {
          case 1:
              
              if (_playerStats.moneyAmount >= 80)
              {
                  npcDialogueReferences.dialogue = brothelMoneyOralBranch;
                  _playerStats.moneyAmount -= 80;
                  sexAnimator.SetTrigger("PayMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() =>
                  {
                      _levelManager.LoadOutBrothel();
                      StartCoroutine(ResetScene());
                  });
              }
              else
              {
                  npcDialogueReferences.dialogue = brothelNoMoneyOralBranch;
                  sexAnimator.SetTrigger("PayNoMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() => StartCoroutine(Reginaldo()));
              }
              break;
          
          case 2:
              
              if (_playerStats.moneyAmount >= 100)
              {
                  npcDialogueReferences.dialogue = brothelMoneyVaginalBranch;
                  _playerStats.moneyAmount -= 100;
                  sexAnimator.SetTrigger("PayMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() =>
                  {
                      _levelManager.LoadOutBrothel();
                      StartCoroutine(ResetScene());
                  });
              }
              else
              {
                  npcDialogueReferences.dialogue = brothelNoMoneyVaginalBranch;
                  sexAnimator.SetTrigger("PayNoMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() => StartCoroutine(Reginaldo()));
              }
              break;

          case 3:
              
              if (_playerStats.moneyAmount >= 120)
              {
                  npcDialogueReferences.dialogue = brothelMoneyAnalBranch;
                  _playerStats.moneyAmount -= 120;
                  sexAnimator.SetTrigger("PayMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() =>
                  {
                      _levelManager.LoadOutBrothel();
                      StartCoroutine(ResetScene());
                  });
              }
              else
              {
                  npcDialogueReferences.dialogue = brothelNoMoneyAnalBranch;
                  sexAnimator.SetTrigger("PayNoMoney");
                  sexDialogueTrigger.TriggerDialogue();
                  _dialogueManager.endedDialogue.AddListener(() => StartCoroutine(Reginaldo()));
              }
              break;
          
        }
        _brothelSounds.StopSounds(STOP_MODE.IMMEDIATE);
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
        yield return new WaitForSeconds(17.5f);
        _levelManager.LoadOutBrothel();
        _brothelSounds.StopSounds(STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(0.5f);
        doorAnimator.Rebind();
        sexAnimator.Rebind();
        sexAnimator.Update(0f);
        npcDialogueReferences.dialogue = firstDialogue;
    }
}