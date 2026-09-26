using NarratorMod.Extensions;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.API;

public class NarratorRegistry
{
    public static Dictionary<string, INarrator> RegisteredNarrators = new();
    public static INarrator? ActiveNarrator { get; private set; }

    public static void RegisterNarrator(INarrator narrator)
    {
        if (RegisteredNarrators.ContainsKey(narrator.Id)) return;

        RegisteredNarrators[narrator.Id] = narrator;

        Logger<NarratorModPlugin>.Info("Narrator registered");
        
        if (ActiveNarrator == null)
        {
            ActiveNarrator = narrator;
            Logger<NarratorModPlugin>.Info("Set active narrator");
        }
    }
    
    public static void SetActiveNarrator(string narratorId)
    {
        if (RegisteredNarrators.TryGetValue(narratorId, out var narrator))
        {
            ActiveNarrator = narrator;
        }
    }
    
    public static void TriggerEvent(string eventKey)
    {
        if (ActiveNarrator == null) return;

        NarrationEntry? entry = ActiveNarrator.GetEntry(eventKey);
        Logger<NarratorModPlugin>.Info("Taking entry");

        foreach (var asset in ActiveNarrator.Database.GetAllEntries())
        {
            Logger<NarratorModPlugin>.Info(asset.Key.Value);
        }
        
        if (entry != null)
        {
            Logger<NarratorModPlugin>.Info("Taking clip from entry");
            AudioClip clip = entry.Clip;
            NarrationManager.AddToQueue(clip);
        }
    }
}