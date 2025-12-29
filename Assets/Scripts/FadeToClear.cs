using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToClear : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed;
    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float currentState = 0;
        while (currentState < 1)
        {
            currentState += Time.deltaTime * fadeSpeed;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, currentState);
            yield return null;
        }
        Destroy(fadeImage.gameObject);
        Destroy(gameObject);
    }
}
