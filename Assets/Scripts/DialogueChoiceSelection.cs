using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

    private void Awake()
    {
        ButtonSelect = new buttonSelectionEvent();
    }

    private void OnEnable()
    {
        if (_inputManager == null)
        {
            _inputManager = FindObjectOfType<InputManager>();
        }
        currentButtonIndex = 0;
        canNavigate = true;
        timer = 0;
        _activeButtons = new List<TextMeshProUGUI>();
        buttons[currentButtonIndex].color = buttonHighlightColor;
        foreach (var button in buttons)
        {
            if (button.gameObject.activeSelf)
            {
                _activeButtons.Add(button);
            }
        }
    }

    private void OnDisable()
    {
        currentButtonIndex = 0;
        canNavigate = false;
        _activeButtons = null;
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            canNavigate = true;
        }
        
        if (_inputManager.NavigationInputDirection.x > 0 && canNavigate)
        {
            if (currentButtonIndex == _activeButtons.Count-1)
            {
                currentButtonIndex = 0;
            }
            else
            {
                currentButtonIndex++;
            }
        }
        else if(_inputManager.NavigationInputDirection.x < 0 && canNavigate)
        {
            if (currentButtonIndex == 0)
            {
                currentButtonIndex = _activeButtons.Count-1;
            }
            else
            {
                currentButtonIndex--;
            }
        }

        if (lastButtonIndex != currentButtonIndex)
        {
            HighlightButton();
            canNavigate = false;
            timer = 0;
        }

        if (_inputManager.ConfirmButton == 1)
        {
            ButtonSelect.Invoke(currentButtonIndex);
        }
        
        lastButtonIndex = currentButtonIndex;
    }

    void HighlightButton()
    {
        buttons[currentButtonIndex].color = buttonHighlightColor;
        buttons[lastButtonIndex].color = buttonColor;
    }


    public class buttonSelectionEvent : UnityEvent<int>
    {
        
    }
}
