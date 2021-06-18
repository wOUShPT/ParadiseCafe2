using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private InputManager _inputManager;
    void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        StartCoroutine(ResetGame());
    }
    

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Credits");
    }
}
