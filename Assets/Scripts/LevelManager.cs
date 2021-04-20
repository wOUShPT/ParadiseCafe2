using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform startSpawn;
    public string previousLevel;
    public string currentLevel;
    private List<DoorTrigger> _doorTriggers;
    private Transform playerSpawnTransform;
    public Transform PlayerSpawnTransform => playerSpawnTransform;
    private bool canLoad;

    private void Awake()
    {
        canLoad = true;
        playerSpawnTransform = FindObjectOfType<ThirdPersonController>().transform;
        currentLevel = SceneManager.GetActiveScene().name;
        playerSpawnTransform = startSpawn;
    }

    private void OnEnable()
    {
        _doorTriggers = FindObjectsOfType<DoorTrigger>().ToList();
    }

    public void LoadLevel(string previousLevelName, string nextLevelName)
    {
        if (canLoad)
        {
            canLoad = false;
            previousLevel = currentLevel;
            SceneManager.LoadScene(nextLevelName);
            StartCoroutine(WaitToLoad());
        }
    }

    public void SpawnOnDoor()
    {
        FindObjectOfType<HUDReferences>().doorsPrompt.SetActive(false);
        
        _doorTriggers = null;
        
        _doorTriggers = FindObjectsOfType<DoorTrigger>().ToList();

        foreach (var _door in _doorTriggers)
        {
            currentLevel = SceneManager.GetActiveScene().name;
            Debug.Log(_door.transform.position.ToString() + " " +  _door.nextSceneName);
            if (_door.nextSceneName == previousLevel)
            {
                Debug.Log("Door");
                Transform playerTransform = FindObjectOfType<ThirdPersonController>().transform;
                playerTransform.position = _door.playerSpawnLocation.position;
                playerTransform.rotation = _door.playerSpawnLocation.rotation;
            }
        }
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1f);
        canLoad = true;
    }
}
