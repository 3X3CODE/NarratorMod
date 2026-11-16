using NarratorMod.Components;
using NarratorMod.Components.Settings;
using NarratorMod.Extensions;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.API;

public class NarratorRegistry
{
    public static List<INarrator> RegisteredNarrators = new();
    public static INarrator? ActiveNarrator { get; private set; }
    public static List<string> AllLanguages = new();

    public static string? ActiveLanguage;
    public static bool AllowSwear;

    public static void RegisterNarrator(INarrator narrator)
    {
        if (RegisteredNarrators.Contains(narrator)) return;

        RegisteredNarrators.Add(narrator);

        Logger<NarratorModPlugin>.Info("Narrator registered");
        
        if (ActiveNarrator == null)
        {
            SetActiveNarrator(narrator);
        }
    }
    
    public static void SetActiveNarrator(INarrator narrator)
    {
        ActiveNarrator = narrator;
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.UpdateNarrator(ActiveNarrator);
        }
        Logger<NarratorModPlugin>.Info("Set active narrator");
    }
    
    public static void NextNarrator()
    {
        if (ActiveNarrator == null) return;
        if (RegisteredNarrators.Count <= 0) return;

        int nextIndex = RegisteredNarrators.IndexOf(ActiveNarrator) + 1;
        if (nextIndex > RegisteredNarrators.Count - 1) nextIndex = 0; 
        
        INarrator nextNarrator = RegisteredNarrators[nextIndex];
        
        SetActiveNarrator(nextNarrator);
    }
    
    public static void PreviousNarrator()
    {
        if (ActiveNarrator == null) return;
        if (RegisteredNarrators.Count <= 0) return;

        int nextIndex = RegisteredNarrators.IndexOf(ActiveNarrator) - 1;
        if (nextIndex < 0) nextIndex = RegisteredNarrators.Count - 1;
        
        INarrator nextNarrator = RegisteredNarrators[nextIndex];
        
        SetActiveNarrator(nextNarrator);
    }

    public static void AddLanguage(string language)
    {
        if (AllLanguages.Contains(language)) return;
        AllLanguages.Add(language);
        ActiveLanguage ??= language;
    }

    public static void NextLanguage()
    {
        if (AllLanguages.Count <= 0) return;
        if (ActiveLanguage == null)
        {
            ActiveLanguage = AllLanguages[0];
            return;
        }
        int index = AllLanguages.IndexOf(ActiveLanguage);
        if (index != -1 && index + 1 < AllLanguages.Count)
        {
            ActiveLanguage = AllLanguages[index + 1];
            return;
        }
        else ActiveLanguage = AllLanguages[0];
    }
    
    public static void PreviousLanguage()
    {
        if (AllLanguages.Count <= 0) return;
        if (ActiveLanguage == null)
        {
            ActiveLanguage = AllLanguages[0];
            return;
        }
        int index = AllLanguages.IndexOf(ActiveLanguage);
        if (index > 0)
        {
            ActiveLanguage = AllLanguages[index - 1];
            return;
        }
        else ActiveLanguage = AllLanguages[AllLanguages.Count - 1];
    }
    
    public static void TriggerEvent(string eventKey, int priority = 0)
    {
        if (ActiveNarrator == null) return;

        NarrationEntryNormal? clip = ActiveNarrator.GetEntry(eventKey);
        if (clip == null)
        {
            Logger<NarratorModPlugin>.Warning($"No narration entry found for key '{eventKey}'");
            return;
        }

        NarrationManager.Enqueue(clip, priority);
    }
}