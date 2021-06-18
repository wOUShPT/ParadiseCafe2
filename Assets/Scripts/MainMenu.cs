using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
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
    private FMOD.Studio.EventInstance _clickSound;

    public void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(Muska);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _clickSound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/SelecionarInteração");

    }
    public void click()
    {
        _clickSound.start();
    }

    public void SelectScene()
    {
        SceneManager.LoadScene(scene1);
    }
    public void SelectScene2()
    {
        SceneManager.LoadScene(scene2);
    }
    public void SelectScene3()
    {
        SceneManager.LoadScene(scene3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
