using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using FMOD;
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
    private ThirdPersonController playerController;
    private Transform playerHoldPoint;
    private Animator _sceneTransitionAnimator;
    private Transform _currentSpawnLocation;
    private DoorController _currentDoorController;
    private HUDReferences _hudReferences;
    private GameObject cineMachineCamera;
    private TimeController _timeController;
    private DailyIncome _dailyIncomeScript;
    private bool canLoad;

    private void Awake()
    {
        _cameraManager = GetComponent<CameraManager>();
        _inputManager = GetComponent<InputManager>();
        playerHoldPoint = GameObject.FindGameObjectWithTag("PlayerHoldPoint").transform;
        canLoad = true;
        playerController = FindObjectOfType<ThirdPersonController>();
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().name;
        _hudReferences = FindObjectOfType<HUDReferences>();
        _timeController = FindObjectOfType<TimeController>();
        _dailyIncomeScript = FindObjectOfType<DailyIncome>();
    }
    

    public void LoadCafe(DoorController doorController, Transform spawnLocation)
    {
        _currentSpawnLocation = spawnLocation;
        _currentDoorController = doorController;
        StartCoroutine(LevelTransition());
    }
    
    

    public void LoadBrodel()
    {
        _timeController.timeFreeze = true;
        _dailyIncomeScript.enabled = false;
        StartCoroutine(TransitionToBrodel());
    }

    public void LoadRape()
    {
        _timeController.timeFreeze = true;
        _dailyIncomeScript.enabled = false;
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
        playerController.RecenterCamera();
        yield return new WaitForSeconds(1.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        previousLevel = _currentDoorController.currentLevelName;
        currentLevel = _currentDoorController.nextLevelName;
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
        _cameraManager.SwitchCamera(currentLevel);
        yield return new WaitForSeconds(1f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        _inputManager.ToggleControls(true);
    }


    IEnumerator TransitionToRape()
    {
        playerController.FreezePlayer(true);
        _inputManager.ToggleControls(false);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerHoldPoint.position;
        _hudReferences.HUDPanel.SetActive(false);
        previousLevel = "Exterior";
        currentLevel = "Rape";
        _cameraManager.SwitchCamera(currentLevel);
        yield return new WaitForSeconds(1f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        _inputManager.ToggleControls(true);
    }
}
