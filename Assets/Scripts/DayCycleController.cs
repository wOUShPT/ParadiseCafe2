using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    private TimeController _timeController;
    public Light directionalLight;
    public Material skyBoxMaterial;
    private Transform sunTransform;
    public LightingPreset preset;

    private void Awake()
    {
        _timeController = FindObjectOfType<TimeController>();
        sunTransform = directionalLight.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            UpdateLightning(_timeController.timePercentage);
        }
    }

    private void UpdateLightning(float timePercentage)
    {
        skyBoxMaterial.SetColor("_HorizonColor", preset.skyColor.Evaluate(timePercentage));
        skyBoxMaterial.SetColor("_SkyColor", preset.skyColor.Evaluate(timePercentage));
        if (timePercentage > 0.25 && timePercentage < 0.75)
        {
            skyBoxMaterial.SetFloat("_StarDensity", 10f);
        }
        else
        {
            skyBoxMaterial.SetFloat("_StarDensity", 0f);
        }

        if (directionalLight != null)
        {
            sunTransform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360f) - 90f, 170, 0));
            directionalLight.transform.localRotation = sunTransform.localRotation;
            skyBoxMaterial.SetVector("_SunDirection", sunTransform.forward);
            skyBoxMaterial.SetVector("_MoonDirection",-sunTransform.forward);
        }
    }
}
