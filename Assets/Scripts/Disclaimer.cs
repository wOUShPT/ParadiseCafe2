using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disclaimer : MonoBehaviour
{
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToLoad());
        
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Menu");
    }
}
