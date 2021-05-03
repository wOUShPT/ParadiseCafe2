using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Loader loader = FindObjectOfType<Loader>();
        if (loader.gameObject != gameObject && loader != null)
        {
            if (SceneManager.GetActiveScene().name == "Choldra")
            {
                Destroy(loader.gameObject);
            }

            if (SceneManager.GetActiveScene().name == "Bordel")
            {
                GameObject.FindGameObjectWithTag("HUD").SetActive(false);
            }
            
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<SetPlayerStats>().ResetStats();
            DontDestroyOnLoad(gameObject);
        }
    }
}
