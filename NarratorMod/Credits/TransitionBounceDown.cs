using Reactor.Utilities.Attributes;
using UnityEngine;
using BepInEx.Unity.IL2CPP.Utils;
using System.Collections;

namespace NarratorMod.Credits;

[RegisterInIl2Cpp]
public class TransitionBounceDown : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 OutOfFramePos;
    public Vector3 InFramePos;
    
    public void OnEnable()
    {
        this.StartCoroutine(AnimateOpen());
        
    }

    public void Close()
    {
        if (base.isActiveAndEnabled)
        {
            this.StartCoroutine(AnimateClose());
        }
    }
    
    // Repurposed from TransitionOpen
    private IEnumerator AnimateClose()
    {
        Vector3 vec = default(Vector3);
        for (float t = 0f; t < this.duration; t += Time.deltaTime)
        {
            float t2 = t / this.duration;
            float num = Mathf.SmoothStep(InFramePos.y, OutOfFramePos.y, t2);
            vec.Set(InFramePos.x, num, InFramePos.z);
            base.transform.position = vec;
            yield return null;
        }
        vec.Set(OutOfFramePos.x, OutOfFramePos.y, OutOfFramePos.z);
        base.transform.position = vec;
        
        yield break;
    }
    
    private IEnumerator AnimateOpen()
    {
        Vector3 vec = default(Vector3);
        for (float t = 0f; t < this.duration; t += Time.deltaTime)
        {
            float t2 = t / this.duration;
            float num = Mathf.SmoothStep(OutOfFramePos.y, InFramePos.y, t2);
            vec.Set(InFramePos.x, num, InFramePos.z);
            base.transform.position = vec;
            yield return null;
        }
        vec.Set(InFramePos.x, InFramePos.y, InFramePos.z);
        base.transform.position = vec;
        
        yield break;
    }
}