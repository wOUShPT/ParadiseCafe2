using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParadiseEnding : MonoBehaviour
{
    public Animator sceneTransition;
    IEnumerator Credits()
    {
        sceneTransition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Credits");
        yield return null;
    }
}