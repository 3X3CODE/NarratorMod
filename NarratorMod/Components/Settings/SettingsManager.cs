using NarratorMod.API;
using NarratorMod.Assets;
using NarratorMod.Components.Subtitles;
using NarratorMod.Extensions;
using NarratorMod.Narrators;
using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using TMPro;
using UnityEngine;

namespace NarratorMod.Components.Settings;

[RegisterInIl2Cpp]
public class SettingsManager : MonoBehaviour
{
    public SettingsManager(IntPtr ptr) : base(ptr) { }
    
    public static SettingsManager? Instance;
    
    public TextMeshPro? narratorNameTMP;
    public TextMeshPro? narratorGenderTMP;
    public TextMeshPro? narratorAuthorTMP;
    public SpriteRenderer? narratorImageRend;

    public PassiveButton? previousNarratorButton;
    public PassiveButton? nextNarratorButton;
    public PassiveButton? playNarratorVoiceButton;
    
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(base.gameObject);
            return;
        }

        Instance = this;

        narratorNameTMP = transform.Find("Scroller/Slider/NarratorSettings/NarratorHeader/NarratorName_TMP")
            .GetComponent<TextMeshPro>();
        narratorGenderTMP = transform.Find("Scroller/Slider/NarratorSettings/NarratorHeader/Gender_TMP")
            .GetComponent<TextMeshPro>();
        narratorAuthorTMP = transform.Find("Scroller/Slider/NarratorSettings/NarratorHeader/Author_TMP")
            .GetComponent<TextMeshPro>();
        narratorImageRend = transform.Find("Scroller/Slider/NarratorSettings/Profile/ProfileImage")
            .GetComponent<SpriteRenderer>();
        
        previousNarratorButton = transform.Find("Scroller/Slider/NarratorSettings/BottomHeader/PreviousButton")
            .GetComponent<PassiveButton>();
        nextNarratorButton = transform.Find("Scroller/Slider/NarratorSettings/BottomHeader/NextButton")
            .GetComponent<PassiveButton>();
        playNarratorVoiceButton = transform.Find("Scroller/Slider/NarratorSettings/BottomHeader/PlayAudioButton")
            .GetComponent<PassiveButton>();

        transform.Find("Scroller/Slider/OtherSettings/LanguageOption").gameObject.AddComponent<SettingsOption>();
        transform.Find("Scroller/Slider/OtherSettings/SwearOption").gameObject.AddComponent<SettingsOption>();
        transform.Find("Scroller/Slider/OtherSettings/SubtitleOption").gameObject.AddComponent<SettingsOption>();
        
        previousNarratorButton.OnClick.RemoveAllListeners();
        nextNarratorButton.OnClick.RemoveAllListeners();
        playNarratorVoiceButton.OnClick.RemoveAllListeners();
        
        nextNarratorButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.NextNarrator();
        }));
        
        previousNarratorButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.PreviousNarrator();
        }));
        
        playNarratorVoiceButton.OnClick.AddListener((Action)(() =>
        {
            PlayTrialAudio();
        }));

        if (NarratorRegistry.ActiveNarrator != null)
        {
            UpdateNarrator(NarratorRegistry.ActiveNarrator);
        }
    }
    
    public void PlayTrialAudio()
    {
        NarratorRegistry.TriggerEvent("voice-trial");
    }

    public void UpdateNarrator(INarrator narrator)
    {
        if (narratorNameTMP != null) narratorNameTMP.text = narrator.Name;
        if (narratorGenderTMP != null) narratorGenderTMP.text = narrator.Gender == NarratorGenders.Male ? "MALE" :
        "FEMALE";
        if (narratorAuthorTMP != null) narratorAuthorTMP.text = $"by {narrator.Author}";
        if (narratorImageRend != null)
        {
            Sprite? image = narrator.ProfileImage;
            if (image != null) narratorImageRend.sprite = image;
        }
    }
    
    public void UpdateSetting(SettingsOption option, bool next)
    {
        switch (option.OptionName)
        {
            case "LanguageOption":
                if (next)
                {
                    NarratorRegistry.NextLanguage();
                }
                else NarratorRegistry.PreviousLanguage();
                option.UpdateValue(NarratorRegistry.ActiveLanguage);
                break;
            case "SwearOption":
                NarratorRegistry.AllowSwear = !NarratorRegistry.AllowSwear;
                option.UpdateValue(NarratorRegistry.AllowSwear ? "Yes" : "No");
                break;
            case "SubtitleOption":
                if (next) SubtitleRegistry.SetNext();
                else SubtitleRegistry.SetPrevious();
                option.UpdateValue(SubtitleRegistry.ActiveSubtitle.GetType().Name);
                break;
        }
    }
}