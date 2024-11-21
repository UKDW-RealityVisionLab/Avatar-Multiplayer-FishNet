using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 3600)] private float TimeOfDay;
    [SerializeField] private Material skyMaterial;

    private void Update()
    {
        if (Preset == null)
        {
            return;
        }
        if(Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 3600;
            UpdateLighting(TimeOfDay / 3600);
            // DateTime utcNow = DateTime.UtcNow; // Waktu UTC
            // TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            // DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);
            // float timeOfDay = (float)localTime.TimeOfDay.TotalSeconds;
            // UpdateLighting(timeOfDay / 86400f);
        }
        else
        {
            UpdateLighting(TimeOfDay/3600);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        if (skyMaterial != null)
            skyMaterial.SetColor("_Tint", Preset.SkyboxColor.Evaluate(timePercent));

        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
