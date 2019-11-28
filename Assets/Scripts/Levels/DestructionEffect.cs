using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionEffect : MonoBehaviour
{
    // Smoke Effect for when the Flammable object burns up

    public ParticleSystem SmokeParticles;
    public ParticleSystem FireParticles;

    void Update()
    {
        Debug.Log(SmokeParticles.isStopped + ", " + FireParticles.isStopped);
        if (SmokeParticles.isStopped && FireParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
