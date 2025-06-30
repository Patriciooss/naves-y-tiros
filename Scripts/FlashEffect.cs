using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image flashImage;
    public float fadeDuration = 0.5f;

    public IEnumerator PlayFlash(float duracionTotal)
    {
        float tiempoPasado = 0f;

        while (tiempoPasado < duracionTotal)
        {
            yield return StartCoroutine(FadeFlash());
        }
    }

    private IEnumerator FadeFlash()
    {
        Color color = flashImage.color;
        color.a = 0f;
        flashImage.color = color;

        // Fade in
        while (flashImage.color.a < 0.1f)
    {
        color.a += Time.unscaledDeltaTime / fadeDuration;
        flashImage.color = color;
        yield return null;
    }

    // Fade out
    while (flashImage.color.a > 0f)
    {
        color.a -= Time.unscaledDeltaTime / fadeDuration;
        flashImage.color = color;
        yield return null;
    }
    }

}