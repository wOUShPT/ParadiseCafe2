using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadScene))]
public class SkipScene : MonoBehaviour
{
    public GameObject skipPrompt;
    private LoadScene _loadSceneScript;
    IEnumerator Start()
    {
        skipPrompt.SetActive(false);
        _loadSceneScript = GetComponent<LoadScene>();
        yield return new WaitForSeconds(4f);
        skipPrompt.SetActive(true);
        yield return null;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            skipPrompt.SetActive(false);
            StartCoroutine(_loadSceneScript.LoadGame());
            enabled = false;
        }
    }

    public void DisableSkip()
    {
        enabled = false;
    }
}
