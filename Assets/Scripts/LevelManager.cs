using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform startSpawn;
    public string previousLevel;
    public string currentLevel;
    private List<DoorController> _doorTriggers;
    private ThirdPersonController playerController;
    private Animator _sceneTransitionAnimator;
    private Transform _currentSpawnLocation;
    private DoorController _currentDoorController;
    public Transform brodelCameraTransform;
    private HUDReferences _hudReferences;
    private Camera mainCamera;
    private GameObject cineMachineCamera;
    private TimeController _timeController;
    private DailyIncome _dailyIncomeScript;
    private bool canLoad;

    private void Awake()
    {
        canLoad = true;
        playerController = FindObjectOfType<ThirdPersonController>();
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().name;
        cineMachineCamera = FindObjectOfType<CinemachineFreeLook>().gameObject;
        _hudReferences = FindObjectOfType<HUDReferences>();
        _timeController = FindObjectOfType<TimeController>();
        _dailyIncomeScript = FindObjectOfType<DailyIncome>();
        mainCamera = Camera.main;
    }

    public void LoadLevel(DoorController doorController, Transform spawnLocation)
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
    

    public void SpawnOnLocation(Transform spawnLocation)
    {
        playerController.transform.position = spawnLocation.position;
        playerController.transform.rotation = spawnLocation.rotation;
    }

    IEnumerator LevelTransition()
    {
        _currentDoorController.isEnabled = false;
        playerController.FreezePlayer(true);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1f);
        SpawnOnLocation(_currentSpawnLocation);
        playerController.RecenterCamera();
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        previousLevel = _currentDoorController.currentLevelName;
        currentLevel = _currentDoorController.nextLevelName;
        playerController.FreezePlayer(false);
        _currentDoorController.isEnabled = true;
        _currentDoorController = null;
        _currentSpawnLocation = null;
    }

    IEnumerator TransitionToBrodel()
    {
        playerController.FreezePlayer(true);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1f);
        cineMachineCamera.SetActive(false);
        _hudReferences.HUDPanel.SetActive(false);
        mainCamera.transform.position = brodelCameraTransform.position;
        mainCamera.transform.rotation = brodelCameraTransform.rotation;
        mainCamera.fieldOfView = 60;
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        previousLevel = _currentDoorController.currentLevelName;
        currentLevel = _currentDoorController.nextLevelName;
    }
}
