using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    private AudioSource audioSource;
    
    public float FadeDuration = .5f;
    public float StartTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator FadeIn()
    {
        yield return null;
        float volume = audioSource.volume;
        audioSource.volume = 0.0f;
        audioSource.loop = true;
        audioSource.time = StartTime;
        audioSource.Play();

        for (float i = 0.0f; i < FadeDuration; i += Time.deltaTime)
        {
            audioSource.volume = volume * i / FadeDuration;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float volume = audioSource.volume;
        for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
        {
            audioSource.volume = volume * i / FadeDuration;
            yield return null;
        }
        audioSource.Stop();
    }
}
