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

    public SettingsOption? languageOption;
    public SettingsOption? swearOption;
    public SettingsOption? subtitleOption;

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

        languageOption = transform.Find("Scroller/Slider/OtherSettings/LanguageOption").gameObject.AddComponent<SettingsOption>();
        swearOption = transform.Find("Scroller/Slider/OtherSettings/SwearOption").gameObject.AddComponent<SettingsOption>();
        subtitleOption = transform.Find("Scroller/Slider/OtherSettings/SubtitleOption").gameObject.AddComponent<SettingsOption>();
        
        languageOption.nextButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.NextLanguage();
        }));
        
        languageOption.previousButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.PreviousLanguage();
        }));
        
        swearOption.nextButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.AllowSwear = !NarratorRegistry.AllowSwear;
            UpdateSwear(NarratorRegistry.AllowSwear);
        }));
        
        swearOption.previousButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.AllowSwear = !NarratorRegistry.AllowSwear;
            UpdateSwear(NarratorRegistry.AllowSwear);
        }));
        
        subtitleOption.nextButton.OnClick.AddListener((Action)(() =>
        {
            UpdateSubtitle(SubtitleRegistry.SetNext());
        }));
        
        subtitleOption.previousButton.OnClick.AddListener((Action)(() =>
        {
            UpdateSubtitle(SubtitleRegistry.SetPrevious());
        }));
        
        NarratorRegistry.NextLanguage();
        
        previousNarratorButton.OnClick.RemoveAllListeners();
        nextNarratorButton.OnClick.RemoveAllListeners();
        
        nextNarratorButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.NextNarrator();
        }));
        
        previousNarratorButton.OnClick.AddListener((Action)(() =>
        {
            NarratorRegistry.PreviousNarrator();
        }));

        if (NarratorRegistry.ActiveNarrator != null)
        {
            UpdateNarrator(NarratorRegistry.ActiveNarrator);
        }
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

    public void UpdateLanguage(string language)
    {
        languageOption.UpdateValue(language);
        Logger<NarratorModPlugin>.Info("language updated");
    }

    public void UpdateSwear(bool allow)
    {
        swearOption.UpdateValue(allow ? "Yes" : "No");
        Logger<NarratorModPlugin>.Info("swear updated");
    }

    public void UpdateSubtitle(string subtitle)
    {
        subtitleOption.UpdateValue(subtitle);
        Logger<NarratorModPlugin>.Info("subtitle updated");
    }
}