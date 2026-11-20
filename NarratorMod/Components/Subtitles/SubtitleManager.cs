using System.Collections;
using Reactor.Utilities.Attributes;
using BepInEx.Unity.IL2CPP.Utils;
using NarratorMod.Extensions;
using NarratorMod.Assets;
using TMPro;
using UnityEngine;

namespace NarratorMod.Components.Subtitles;

[RegisterInIl2Cpp]
public class SubtitleManager : MonoBehaviour
{
    public SubtitleManager(IntPtr ptr) : base(ptr) { }

    public static SubtitleManager? Instance;
    public static Coroutine? SubtitleRoutine;
    public static ISubtitle? LastSubtitle;

    public static GameObject? SubtitleContainer;
    public static TextMeshPro? NarratorTMP;
    public static TextMeshPro? TextTMP;
    
    public static Vector3 DefaultPos { get; set; } = new Vector3(0f, -1.5f, -500);
    public static Vector3 DefaultScale { get; set; } = new Vector3(0.1f, 0.1f, 0.1f);
    
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;

        SubtitleContainer = Instantiate(PresetAssets.SubtitlePrefab, transform);
        NarratorTMP = SubtitleContainer.transform.Find("NarratorTMP").GetComponent<TextMeshPro>();
        TextTMP = SubtitleContainer.transform.Find("TextTMP").GetComponent<TextMeshPro>();
        NarratorTMP.gameObject.layer = LayerMask.NameToLayer("UI");
        TextTMP.gameObject.layer = LayerMask.NameToLayer("UI");
        var exampleText = HudManager.Instance.roomTracker.text;
        TextTMP.font = NarratorTMP.font = exampleText.font;
        TextTMP.fontMaterial = NarratorTMP.fontMaterial = new Material(exampleText.fontMaterial);
        ResetState();
    }

    public void StartSubtitle(string text, string narrator, float duration, ISubtitle? subtitle = null)
    {
        subtitle ??= LastSubtitle;
        if (subtitle == null) return;
        
        SubtitleRoutine = this.StartCoroutine(StartSubtitleCoroutine(subtitle, text, narrator));
    }

    public void StopSubtitle()
    {
        if (SubtitleRoutine != null) StopCoroutine(SubtitleRoutine);
        this.StartCoroutine(EndSubtitleCoroutine());
    }

    public IEnumerator StartSubtitleCoroutine(ISubtitle subtitle, string text, string author)
    {
        if (SubtitleContainer == null || NarratorTMP == null || TextTMP == null) yield break;

        LastSubtitle = subtitle;
        
        ResetState();

        NarratorTMP.text = $"[{author}]";
        
        this.StartCoroutine(subtitle.TransitionIn(SubtitleContainer));
        yield return this.StartCoroutine(subtitle.TextAnimation(text, TextTMP));
        //yield return new WaitForSeconds(duration);
        //yield return this.StartCoroutine(subtitle.TransitionOut(SubtitleContainer));
        //ResetState();
    }
    
    public IEnumerator EndSubtitleCoroutine()
    {
        if (SubtitleContainer == null || LastSubtitle == null) yield break;
        if (SubtitleRoutine != null) StopCoroutine(SubtitleRoutine);
        yield return this.StartCoroutine(LastSubtitle.TransitionOut(SubtitleContainer));
        ResetState();
    }
    
    public static void ResetState()
    {
        if (SubtitleContainer == null || NarratorTMP == null || TextTMP == null) return;
        NarratorTMP.text = string.Empty;
        TextTMP.text = string.Empty;
        NarratorTMP.alpha = 1f;
        TextTMP.alpha = 1f;
        SubtitleContainer.SetActive(false);
        SubtitleContainer.transform.localScale = DefaultScale;
        SubtitleContainer.transform.localPosition = DefaultPos;
    }
}