using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayCycleController : MonoBehaviour
{
    private TimeController _timeController;
    public Volume _postProcessVolume;
    private Bloom _bloom;
    public Light sunLight;
    public Transform sunPivotTransform;
    public Material skyBoxMaterial;
    public LightingPreset colorsPreset;
    public AnimationCurve starsOpacityOvertime;

    private void Awake()
    {
        _timeController = FindObjectOfType<TimeController>();
        Bloom tmpBloom;
        if (_postProcessVolume.profile.TryGet(out tmpBloom))
        {
            _bloom = tmpBloom;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colorsPreset == null)
        {
            return;
        }
        
        UpdateLightning(_timeController.TimePercentage);
    }

    private void UpdateLightning(float timePercentage)
    {
        sunLight.color = colorsPreset.lightColor.Evaluate(timePercentage);
        skyBoxMaterial.SetColor("_HorizonColor", colorsPreset.skyColor.Evaluate(timePercentage));
        skyBoxMaterial.SetColor("_SkyColor", colorsPreset.skyColor.Evaluate(timePercentage));
        skyBoxMaterial.SetFloat("_StarsOpacity", starsOpacityOvertime.Evaluate(timePercentage));
        if (timePercentage > 0.25 && timePercentage < 0.75)
        {
            _bloom.intensity.value = 0f;
        }
        else
        {
            _bloom.intensity.value = 0.01f;
        }

        if (sunLight != null)
        {
            sunPivotTransform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360f) - 90f, 0, 0));
            skyBoxMaterial.SetVector("_SunDirection", sunPivotTransform.forward);
            skyBoxMaterial.SetVector("_MoonDirection",-sunPivotTransform.forward);
        }
    }
}
