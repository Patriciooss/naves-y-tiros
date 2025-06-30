using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    
    public static DamageFlash instance;
    private Image flashImage;

    void Awake()
    {
        instance = this;
        flashImage = GetComponent<Image>();
        
    }

    public void Flash(float duration)
    {
        StartCoroutine(FlashCoroutine(duration));
    }

    IEnumerator FlashCoroutine(float duration)
    {
        flashImage.enabled = true;

        Color color = flashImage.color;
        color.a = 1f;
        flashImage.color = color;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / duration);
            flashImage.color = color;
            yield return null;
        }

        flashImage.enabled = false;
    }
}

