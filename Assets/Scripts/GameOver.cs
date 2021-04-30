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
    }
    
    void Update()
    {
        if (_inputManager.ConfirmButton == 1)
        {
            SceneManager.LoadScene("Gustavo");
        }
    }
}
