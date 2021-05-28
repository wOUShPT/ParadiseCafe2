using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;

public class DialogueChoiceSelection : MonoBehaviour
{
    public List<TextMeshProUGUI> buttons;
    private List<TextMeshProUGUI> _activeButtons;
    public Color buttonColor;
    public Color buttonHighlightColor;
    private InputManager _inputManager;
    private int currentButtonIndex;
    private int lastButtonIndex;
    private bool canNavigate;
    public buttonSelectionEvent ButtonSelect;
    private float timer;
    private FMOD.Studio.EventInstance changeOptionSound;
    private FMOD.Studio.EventInstance selectOptionSound;

    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        ButtonSelect = new buttonSelectionEvent();
        changeOptionSound = FMODUnity.RuntimeManager.CreateInstance("event:/MudarDeInteração");
        selectOptionSound = FMODUnity.RuntimeManager.CreateInstance("event:/SelecionarInteração");
    }

    private void OnEnable()
    {
        
    }
    
    public void ResetButtons()
    {
        lastButtonIndex = 0;
        currentButtonIndex = 0;
        canNavigate = false;
        _activeButtons = null;
        timer = 0;
        foreach (var button in buttons)
        {
            button.text = "";
            button.color = buttonColor;
        }
    }

    void Update()
    {
        
    }

    void HighlightButton()
    {
        buttons[currentButtonIndex].color = buttonHighlightColor;
        buttons[lastButtonIndex].color = buttonColor;
    }

    public IEnumerator Selection()
    {
        if (_inputManager == null)
        {
            _inputManager = FindObjectOfType<InputManager>();
        }
        lastButtonIndex = 0;
        currentButtonIndex = 0;
        canNavigate = false;
        timer = 0;
        _activeButtons = new List<TextMeshProUGUI>();
        foreach (var button in buttons)
        {
            if (button.gameObject.activeSelf)
            {
                _activeButtons.Add(button);
            }
        }
        buttons[currentButtonIndex].color = buttonHighlightColor;
        
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.2f)
            {
                canNavigate = true;
            }
        
            if (_inputManager.NavigationInputDirection.y < 0 && canNavigate)
            {
                changeOptionSound.start();
                if (currentButtonIndex == _activeButtons.Count-1)
                {
                    currentButtonIndex = 0;
                }
                else
                {
                    currentButtonIndex++;
                }
                
                HighlightButton();
                canNavigate = false;
                timer = 0;
                
            }
            else if(_inputManager.NavigationInputDirection.y > 0 && canNavigate)
            {
                changeOptionSound.start();
                if (currentButtonIndex == 0)
                {
                    currentButtonIndex = _activeButtons.Count-1;
                }
                else
                {
                    currentButtonIndex--;
                }
                
                HighlightButton();
                canNavigate = false;
                timer = 0;
            }
            

            if (_inputManager.ConfirmButton == 1)
            {
                selectOptionSound.start();
                ButtonSelect.Invoke(currentButtonIndex);
                StopAllCoroutines();
            }
        
            lastButtonIndex = currentButtonIndex;
            yield return null;
        }
    }


    public class buttonSelectionEvent : UnityEvent<int>
    {
        
    }
}
