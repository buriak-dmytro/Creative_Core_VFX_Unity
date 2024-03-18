using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LightningElementControl : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Space;

    public ParticleSystem particle;
    public Material material;

    public Color emissionColor;
    public float emissionIntensity = 2.0f;
    public float emissionToggleTime = 2.0f;

    private bool isEmissionIncreasing = false;
    private bool isEmissionDecreasing = false;
    private float counterIntensity = 0;
    private float counterTime = 0;

    private bool isPlayingParticleSystem = true;

    void Start()
    {
        if (particle.isPlaying)
        {
            particle.Play();
            material.SetColor("_EmissionColor", emissionColor * emissionIntensity);
            isPlayingParticleSystem = true;
        }
        else
        {
            particle.Stop();
            material.SetColor("_EmissionColor", Color.black);
            isPlayingParticleSystem = false;
        }
    }

    void Update()
    {
        if (isEmissionIncreasing)
        {
            EmissionIncreasing();
        }
        else if (isEmissionDecreasing)
        {
            EmissionDecreasing();
        }
        else
        {
            if (Input.GetKeyDown(toggleKey))
            {
                if (isPlayingParticleSystem)
                {
                    particle.Stop();
                    isEmissionDecreasing = true;
                    isPlayingParticleSystem = false;
                }
                else
                {
                    particle.Play();
                    isEmissionIncreasing = true;
                    isPlayingParticleSystem = true;
                }
            }
        }
    }

    private void EmissionIncreasing()
    {
        if (counterTime < emissionToggleTime)
        {
            counterTime += Time.deltaTime;
            counterIntensity =
                emissionIntensity * counterTime / emissionToggleTime;
            material.SetColor("_EmissionColor", emissionColor * counterIntensity);
        }
        else
        {
            counterTime = 0.0f;
            isEmissionIncreasing = false;
        }
    }

    private void EmissionDecreasing()
    {
        if (counterTime < emissionToggleTime)
        {
            counterTime += Time.deltaTime;
            counterIntensity =
                emissionIntensity -
                emissionIntensity * counterTime / emissionToggleTime;
            material.SetColor("_EmissionColor", emissionColor * counterIntensity);
        }
        else
        {
            counterTime = 0.0f;
            isEmissionDecreasing = false;
        }
    }
}
