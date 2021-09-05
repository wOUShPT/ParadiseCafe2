using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlsScreen : MonoBehaviour
{
    private InputManager _inputManager;
    public GameObject controlsScreen;
    public QuitGame _quitGame;
    IEnumerator Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckInput());
    }
    

    IEnumerator CheckInput()
    {
        controlsScreen.SetActive(true);
        Time.timeScale = 0;
        while (true)
        {
            if (_inputManager.EscapeButton == 1)
            {
                controlsScreen.SetActive(false);
                Time.timeScale = 1;
                _quitGame.enabled = true;
                enabled = false;
            }
            yield return new WaitForSecondsRealtime(0.01f);
            yield return null;
        }
    }
}
