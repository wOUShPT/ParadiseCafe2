using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newDialogue", menuName = "Dialogue", order = 1)]
public class Dialogue: ScriptableObject
{
    public string characterName;

    public Dialogue nextDialogue;

    public  bool hasChoices;
    
    [TextArea(3, 19)]
    public string sentence;

    public List<Choice> choices;
}

