using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ResetGame());
    }
    

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Exterior");
    }
}
