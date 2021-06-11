using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CameraManager _cameraManager;
    private InputManager _inputManager;
    public Transform startSpawn;
    public string previousLevel;
    public string currentLevel;
    private List<DoorController> _doorTriggers;
    private SexDialogueTrigger _sexDialogueTrigger;
    private ThirdPersonController playerController;
    private Transform playerHoldPoint;
    private Transform casaDaVelhaExterior;
    private Animator _sceneTransitionAnimator;
    private Transform _currentSpawnLocation;
    private DoorController _currentDoorController;
    private HUDReferences _hudReferences;
    private GameObject cineMachineCamera;
    private TimeController _timeController;
    private DailyIncome _dailyIncomeScript;
    
    private FMOD.Studio.EventInstance exteriorSnapshot;
    private FMOD.Studio.EventInstance cafeSnapshot;
    private FMOD.Studio.EventInstance brodelSnapshot;
    private FMOD.Studio.EventInstance rapeSnapshot;

    private void Awake()
    {
        _cameraManager = GetComponent<CameraManager>();
        _inputManager = GetComponent<InputManager>();
        casaDaVelhaExterior = GameObject.FindGameObjectWithTag("VelhaGate").transform;
        playerHoldPoint = GameObject.FindGameObjectWithTag("PlayerHoldPoint").transform;
        playerController = FindObjectOfType<ThirdPersonController>();
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().name;
        _hudReferences = FindObjectOfType<HUDReferences>();
        _timeController = FindObjectOfType<TimeController>();
        _dailyIncomeScript = FindObjectOfType<DailyIncome>();
        _sexDialogueTrigger = FindObjectOfType<SexDialogueTrigger>();

        cafeSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/InteriorCafé");
        brodelSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/InteriorBordel");
        rapeSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/InteriorCasaVelha");
        exteriorSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Exterior");
        exteriorSnapshot.start();
    }
    

    public void LoadCafe(DoorController doorController, Transform spawnLocation)
    {
        _currentSpawnLocation = spawnLocation;
        _currentDoorController = doorController;
        StartCoroutine(LevelTransition());
    }
    
    

    public void LoadBrodel()
    {
        StartCoroutine(TransitionToBrodel());
    }

    public void LoadRape()
    {
        StartCoroutine(TransitionToRape());
    }
    

    public void SpawnOnLocation(Transform spawnLocation)
    {
        playerController.transform.position = spawnLocation.position;
        playerController.transform.rotation = spawnLocation.rotation;
    }

    IEnumerator LevelTransition()
    {
        _currentDoorController.isEnabled = false;
        playerController.FreezePlayer(true);
        _inputManager.ToggleControls(false);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        SpawnOnLocation(_currentSpawnLocation);
        previousLevel = _currentDoorController.currentLevelName;
        currentLevel = _currentDoorController.nextLevelName;
        SwitchFMODSnapshot();
        playerController.RecenterCamera();
        yield return new WaitForSeconds(1.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        playerController.FreezePlayer(false);
        _inputManager.ToggleControls(true);
        _currentDoorController.isEnabled = true;
        _currentDoorController = null;
        _currentSpawnLocation = null;
    }

    IEnumerator TransitionToBrodel()
    {
        playerController.FreezePlayer(true);
        _inputManager.ToggleControls(false);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerHoldPoint.position;
        _hudReferences.HUDPanel.SetActive(false);
        previousLevel = "Exterior";
        currentLevel = "Brodel";
        SwitchFMODSnapshot();
        _cameraManager.SwitchCamera(currentLevel);
        yield return new WaitForSeconds(1f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        _inputManager.ToggleControls(true);
        _sexDialogueTrigger.TriggerDialogue();
    }


    IEnumerator TransitionToRape()
    {
        _timeController.timeFreeze = true;
        _dailyIncomeScript.enabled = false;
        _timeController.timeFreeze = true;
        _dailyIncomeScript.enabled = false;
        playerController.FreezePlayer(true);
        _inputManager.ToggleControls(false);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerHoldPoint.position;
        _hudReferences.HUDPanel.SetActive(false);
        previousLevel = "Exterior";
        currentLevel = "Rape";
        SwitchFMODSnapshot();
        _cameraManager.SwitchCamera(currentLevel);
        yield return new WaitForSeconds(1f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(10f);
        StartCoroutine(TransitionToExterior());
    }

    IEnumerator TransitionToExterior()
    {
        _inputManager.ToggleControls(false);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        playerController.transform.position = casaDaVelhaExterior.transform.position;
        playerController.transform.rotation = casaDaVelhaExterior.transform.rotation;
        previousLevel = currentLevel;
        currentLevel = "Exterior";
        SwitchFMODSnapshot();
        _cameraManager.SwitchCamera(currentLevel);
        playerController.RecenterCamera();
        yield return new WaitForSeconds(1f);
        _hudReferences.HUDPanel.SetActive(true);
        _timeController.timeFreeze = false;
        if (previousLevel == "Brodel")
        {
            _timeController.inGameTime = 0.35f;
        }

        if (previousLevel == "Rape")
        {
            _timeController.inGameTime = 0.65f;
        }
        _dailyIncomeScript.enabled = true;
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        _inputManager.ToggleControls(true);
        playerController.FreezePlayer(false);
    }

    void SwitchFMODSnapshot()
    {
        switch (currentLevel)
        {
            case "Exterior":
                
                cafeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                brodelSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                rapeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                exteriorSnapshot.start();
                
                break;
            
            case "Café":
                
                exteriorSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                brodelSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                rapeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                cafeSnapshot.start();
                
                break;
            
            case "Brodel":
                
                cafeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                exteriorSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                rapeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                brodelSnapshot.start();
                
                break;
            
            case "Rape":
                
                brodelSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                cafeSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                exteriorSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
                rapeSnapshot.start();
                
                break;
        }
    }
}
