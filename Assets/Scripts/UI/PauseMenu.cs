using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputManager _inputManager;
    public GameObject pauseScreen;
    private bool _isPaused;
    IEnumerator Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _isPaused = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(CheckInput());
    }

    void TogglePause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    IEnumerator CheckInput()
    {
        while (true)
        {
            if (_inputManager.EscapeButton == 1)
            {
                TogglePause();
            }
            yield return new WaitForSecondsRealtime(0.1f);
            yield return null;
        }
    }
}
