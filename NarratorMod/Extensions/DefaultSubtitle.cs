using System.Collections;
using TMPro;
using UnityEngine;

namespace NarratorMod.Extensions;

public class DefaultSubtitle : ISubtitle
{
    public IEnumerator TransitionIn(GameObject container)
    {
        container.SetActive(true);
        TextMeshPro[] textComponents = container.GetComponentsInChildren<TextMeshPro>();
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            
            foreach (var tmp in textComponents)
            {
                tmp.alpha = alpha;
            }

            yield return null;
        }
    }
    
    public IEnumerator TextAnimation(string text, TextMeshPro textMeshPro)
    {
        textMeshPro.text = text;
        yield break;
    }
    
    public IEnumerator TransitionOut(GameObject container)
    {
        TextMeshPro[] textComponents = container.GetComponentsInChildren<TextMeshPro>();
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            
            foreach (var tmp in textComponents)
            {
                tmp.alpha = alpha;
            }

            yield return null;
        }
    }
}