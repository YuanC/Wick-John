using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Scripts for fade-able objects
public class Fade : MonoBehaviour
{
    private bool transitioning;

    public float FadeDuration = .5f;
    public bool FadeOutAtStart = false;

    public Image TransitionImage;

    void Start()
    {
        transitioning = false;

        if (FadeOutAtStart)
        {
            TransitionImage.gameObject.SetActive(true);
            StartCoroutine(FadeTransitionImageOut());
        }
    }

    public void SetTransparent()
    {
        Color color;
        bool isText = GetComponent<Text>() != null;
        color = isText ? GetComponent<Text>().color : GetComponent<Image>().color;
        color.a = 0;
        if (isText) { GetComponent<Text>().color = color; }
        else { GetComponent<Image>().color = color; }
    }

    public void SetOpaque()
    {
        Color color;
        bool isText = GetComponent<Text>() != null;
        color = isText ? GetComponent<Text>().color : GetComponent<Image>().color;
        color.a = 1;
        if (isText) { GetComponent<Text>().color = color; }
        else { GetComponent<Image>().color = color; }
    }

    public IEnumerator FadeIn()
    {
        Color color;
        bool isText = GetComponent<Text>() != null;
        color = isText ? GetComponent<Text>().color : GetComponent<Image>().color;

        for (float i = 0; i <= FadeDuration; i += Time.deltaTime)
        {
            color.a = i / FadeDuration;
            if (isText) { GetComponent<Text>().color = color; }
            else { GetComponent<Image>().color = color; }
            yield return null;
        }
        color.a = 1;
        if (isText) { GetComponent<Text>().color = color; }
        else { GetComponent<Image>().color = color; }
    }

    public IEnumerator FadeOut()
    {
        Color color;
        bool isText = GetComponent<Text>() != null;
        color = isText ? GetComponent<Text>().color : GetComponent<Image>().color;

        for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
        {
            color.a = i / FadeDuration;
            if (isText) { GetComponent<Text>().color = color; }
            else { GetComponent<Image>().color = color; }
            yield return null;
        }
        color.a = 0;
        if (isText) { GetComponent<Text>().color = color; }
        else { GetComponent<Image>().color = color; }
    }

    public IEnumerator FadeInAndOut()
    {

        Color color;
        bool isText = GetComponent<Text>() != null;
        color = isText ? GetComponent<Text>().color : GetComponent<Image>().color;

        bool fadingIn = true;
        float i = 0f;

        while (true)
        {
            if (i >= FadeDuration)
            {
                fadingIn = !fadingIn;
                i = 0f;
            }

            color.a = fadingIn ? i / FadeDuration : 1 - i / FadeDuration;
            if (isText) { GetComponent<Text>().color = color; }
            else { GetComponent<Image>().color = color; }
            yield return null;

            i += Time.deltaTime;
        }
    }

    public IEnumerator FadeTransitionImageOut()
    {
        Color color = TransitionImage.color;

        for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
        {
            color.a = i / FadeDuration;
            TransitionImage.color = color;
            yield return null;
        }
        color.a = 0;
        TransitionImage.color = color;
        TransitionImage.gameObject.SetActive(false);
    }

    public IEnumerator TransitionToScene(string sceneName)
    {
        if (!transitioning)
        {
            transitioning = true;
            Color color = TransitionImage.color;
            TransitionImage.gameObject.SetActive(true);

            for (float i = 0; i <= FadeDuration; i += Time.deltaTime)
            {
                color.a = i / FadeDuration;
                TransitionImage.color = color;
                yield return null;
            }
            color.a = 1;
            TransitionImage.color = color;
            yield return null;

            SceneManager.LoadScene(sceneName);
        }
    }
}
