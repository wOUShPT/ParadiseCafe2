using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private InputManager _inputManager;
    public GameObject resetGamePrompt;
    public GameObject goMenuPrompt;
    void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        StartCoroutine(ResetGame());
    }
    

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5f);
        resetGamePrompt.SetActive(true);
        goMenuPrompt.SetActive(true);
        while (true)
        {
            if (_inputManager.ActionInput == 1)
            {
                SceneManager.LoadScene("Exterior");
                break;
            }
            
            if (_inputManager.EscapeButton == 1)
            {
                SceneManager.LoadScene("Menu");
                break;
            }

            yield return null;
        }
    }
}
