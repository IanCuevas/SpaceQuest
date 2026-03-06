using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;
    [SerializeField] private Image screenFade;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // Ensure initial transparency
        if (screenFade != null)
        {
            Color c = screenFade.color;
            c.a = 0f;
            screenFade.color = c;
        }
    }

    public void BeginScreenFade(float duration)
    {
        if (screenFade != null)
            StartCoroutine(FadeScreen(duration));
    }

    private IEnumerator FadeScreen(float duration)
    {
        // Immediately set screen to black
        SetAlpha(1f);

        float timer = 0f;
        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            SetAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends fully transparent
        SetAlpha(0f);
    }


    private void SetAlpha(float alpha)
    {
        if (screenFade != null)
        {
            Color c = screenFade.color;
            c.a = alpha;
            screenFade.color = c;
        }
    }
}
