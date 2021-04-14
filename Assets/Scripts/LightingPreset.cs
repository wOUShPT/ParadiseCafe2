using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lightning Preset", menuName = "Scriptable Objects/Lightning Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient skyColor;
}
