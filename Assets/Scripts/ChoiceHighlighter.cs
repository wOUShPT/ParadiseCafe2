using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

public class ChoiceHighlighter : MonoBehaviour
{
    private Button _button;
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _text = _button.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeColor(Color color)
    {
        _text.color = color;
    }
}
