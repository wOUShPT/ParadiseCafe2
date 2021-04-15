using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private AudioClip clickes;
    [SerializeField] private AudioClip Muska;
    [SerializeField] private string scene1;
    [SerializeField] private string scene2;
    [SerializeField] private string scene3;

    public void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(Muska);
    }
    public void click()
    {
        AudioManager.Instance.PlaySFX(clickes);
    }

    public void SelectScene()
    {
        SceneManager.LoadScene(scene1);
        AudioManager.Instance.StopMusic();
    }
    public void SelectScene2()
    {
        SceneManager.LoadScene(scene2);
        AudioManager.Instance.StopMusic();
    }
    public void SelectScene3()
    {
        SceneManager.LoadScene(scene3);
        AudioManager.Instance.StopMusic();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
