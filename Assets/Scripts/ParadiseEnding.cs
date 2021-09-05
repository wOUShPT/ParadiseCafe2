using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParadiseEnding : MonoBehaviour
{
    IEnumerator Credits()
    {
        SceneManager.LoadScene("Credits");
        yield return null;
    }
}