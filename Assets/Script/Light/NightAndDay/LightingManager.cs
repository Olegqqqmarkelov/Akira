using UnityEngine;
using System;
using System.Collections;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] public float speed;

    private void Start()
    {
        StartCoroutine(TrackFog());
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime * speed;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    private IEnumerator TrackFog()
    {
        float fog;
        while(true)
        {
            if (TimeOfDay>=18 && TimeOfDay <= 19)
            {
                fog = (float)Math.Sin((TimeOfDay - 18) * 5);
                if (fog >= 0.15f)
                {
                    RenderSettings.fogDensity = 0.15f;
                    yield return new WaitForSeconds(75f);
                }
                else {
                    RenderSettings.fogDensity = fog;
                }
            }
            else if (TimeOfDay >= 3f && TimeOfDay <= 4f)
            {
                fog = (float)Math.Sin((TimeOfDay - 3) * 5);
                if (fog >= 0)
                {
                    RenderSettings.fogDensity = 0.15f;
                    yield return new WaitForSeconds(75f);
                }
                else
                {
                    RenderSettings.fogDensity = fog;
                }
            }
            else
            {
                yield return new WaitForSeconds(5f);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}