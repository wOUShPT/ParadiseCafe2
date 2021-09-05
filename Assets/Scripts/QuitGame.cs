using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private InputManager _inputManager;
    void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        StartCoroutine(CheckInput());
    }

    IEnumerator CheckInput()
    {
        while (true)
        {
            if (_inputManager.QuitButton == 1)
            {
                Debug.Log("QuitGame");
                Application.Quit();
                enabled = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
