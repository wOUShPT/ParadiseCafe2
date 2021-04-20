using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class DoorTrigger : MonoBehaviour
{
    public string nextSceneName;
    public string currentSceneName;
    public Transform playerSpawnLocation;
    private GameObject _promptText;
    private BoxCollider _trigger;
    private InputManager _inputManager;
    private LevelManager _levelManager;
    private StringEvent _doorTriggered;
    IEnumerator Start()
    {
        _trigger = GetComponent<BoxCollider>();
        _trigger.isTrigger = true;
        _trigger.enabled = false;
        _promptText = FindObjectOfType<HUDReferences>().doorsPrompt;
        _promptText.SetActive(false);
        _inputManager = FindObjectOfType<InputManager>();
        yield return new WaitForSeconds(1f);
        _trigger.enabled = true;
    }

    private void OnEnable()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _doorTriggered = new StringEvent();
        _doorTriggered.AddListener(_levelManager.LoadLevel);
    }

    private void OnTriggerStay(Collider other)
    {
        _promptText.SetActive(false);
        if (other.CompareTag("Player"))
        {
            _promptText.SetActive(true);
            if (_inputManager.ActionInput == 1)
            {
                _doorTriggered.Invoke(currentSceneName, nextSceneName);
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

    private void OnDisable()
    {
        _doorTriggered.RemoveAllListeners();
    }
}

public class StringEvent : UnityEvent<string, string>
{
    
}
