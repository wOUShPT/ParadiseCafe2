using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelationAdjust : MonoBehaviour
{
    public Material _material;
    private MaterialPropertyBlock mpb;
    private void Awake()
    {
        _material.SetFloat("_DitherAmount", 1.23f);
    }
    
}
