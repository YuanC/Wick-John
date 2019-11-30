using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epigraph : MonoBehaviour
{
    public Fade Sentence1;
    public Fade Sentence2;
    public Fade QuoteBG;
    public Flammable Candle;
    public Fade Title1;
    public Fade Title2;
    public Fade ContinuePrompt;

    public Fade SceneTransition;

    // Initial state for all objects
    void Start()
    {
        Sentence1.SetTransparent();
        Sentence2.SetTransparent();
        QuoteBG.gameObject.SetActive(true);
        QuoteBG.SetOpaque();
        Title1.SetTransparent();
        Title2.SetTransparent();
        ContinuePrompt.SetTransparent();
        Candle.isLit = false;

        StartCoroutine(IntroAnimation());
    }


    private IEnumerator IntroAnimation()
    {
        yield return new WaitForSeconds(.5f);

        StartCoroutine(Sentence1.FadeIn());

        yield return new WaitForSeconds(2f);

        StartCoroutine(Sentence2.FadeIn());

        yield return new WaitForSeconds(3f);

        StartCoroutine(Sentence1.FadeOut());
        StartCoroutine(Sentence2.FadeOut());

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(QuoteBG.FadeOut());

        yield return new WaitForSeconds(1f);

        Candle.isLit = true;

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(Title1.FadeIn());

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Title2.FadeIn());

        yield return new WaitForSeconds(1f);

        StartCoroutine(ContinuePrompt.FadeInAndOut());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.anyKey)
        {
            // Skip transition animation
            StartCoroutine(SceneTransition.TransitionToScene("Level Select"));
        }
    }
}
