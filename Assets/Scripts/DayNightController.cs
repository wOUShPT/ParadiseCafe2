using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Light directionalLight;
    public LightingPreset preset;
    [Range(0,24)]
    public float timeOfDay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_SunDirection", directionalLight.transform.forward);
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime/60;
            timeOfDay %= 24;
            UpdateLightning(timeOfDay);
        }
    }

    private void UpdateLightning(float timePercentage)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercentage);

        if (directionalLight != null)
        {
            //directionalLight.color = preset.directionalColor.Evaluate(timePercentage);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360f) - 90f, 170f, 0));
        }
    }

    /*private void OnValidate()
    {
        if (directionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (var light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
            
    }*/
}
