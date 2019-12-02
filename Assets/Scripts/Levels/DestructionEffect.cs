using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dramatic Smoke Effect for when the Flammable object burns up
public class DestructionEffect : MonoBehaviour
{
    public ParticleSystem SmokeParticles;
    public ParticleSystem FireParticles;

    void Update()
    {
        if (SmokeParticles.isStopped && FireParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
