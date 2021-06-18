using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class DoorController : MonoBehaviour
{
    public string nextLevelName;
    public string currentLevelName;
    public Transform destinationSpawnLocation;
    private GameObject _promptText;
    private BoxCollider _trigger;
    private InputManager _inputManager;
    private LevelManager _levelManager;
    private StringEvent _doorTriggered;
    public bool isEnabled;
    private FMOD.Studio.EventInstance _doorOpenSound;
    void Start()
    {
        _trigger = GetComponent<BoxCollider>();
        _trigger.isTrigger = true;
        _promptText = FindObjectOfType<HUDReferences>().doorsPrompt;
        _promptText.SetActive(false);
        _inputManager = FindObjectOfType<InputManager>();
        _trigger.enabled = true;
        isEnabled = true;
        _doorOpenSound = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/OpçãoEntrarSair");
    }

    private void OnEnable()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _doorTriggered = new StringEvent();
        _doorTriggered.AddListener(_levelManager.LoadCafe);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEnabled)
        {
            _promptText.SetActive(false);
            if (other.CompareTag("Player"))
            {
                _promptText.SetActive(true);
                if (_inputManager.ActionInput == 1)
                {
                    _doorOpenSound.start();
                    _doorTriggered.Invoke(this, destinationSpawnLocation);
                    _promptText.SetActive(false);
                    isEnabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _promptText.SetActive(false);
        }
    }
    
}

public class StringEvent : UnityEvent<DoorController, Transform>
{
    
}
