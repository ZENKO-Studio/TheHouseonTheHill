using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup canvasGroup; // Reference to the Canvas Group component
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade-out effect

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        StartFadeOut();
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;

            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
