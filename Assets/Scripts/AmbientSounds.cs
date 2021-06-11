using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance _cricketsSound;
    private FMOD.Studio.EventInstance _birdsSound;
    void Start()
    {
        _cricketsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Grilos");
        _birdsSound = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Pássaros");
    }
}
