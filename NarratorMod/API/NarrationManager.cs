using System;
using System.Collections;
using System.Collections.Generic;
using NarratorMod.Components;
using NarratorMod.Components.Subtitles;
using NarratorMod.Extensions;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.API;

public static class NarrationManager
{
    private static AudioSource Source;
    
    private class QueueItem
    {
        public NarrationEntryNormal? Clip;
        public int Priority;
    }

    private static readonly List<QueueItem> Queue = new();
    public static event Action<NarrationEntryNormal>? OnClipStarted;
    public static event Action<NarrationEntryNormal>? OnClipFinished;

    public static void Enqueue(NarrationEntryNormal clip, int priority = 0)
    {
        if (clip?.Clip == null)
        {
            Logger<NarratorModPlugin>.Warning($"Tried to queue narration '{clip?.Key}' with no audio clip.");
            return;
        }

        var item = new QueueItem { Clip = clip, Priority = priority };

        // Insert sorted by priority, highest first, stable relative to existing entries.
        int insertIndex = Queue.FindIndex(q => q.Priority < priority);
        if (insertIndex == -1)
            Queue.Add(item);
        else
            Queue.Insert(insertIndex, item);

        Logger<NarratorModPlugin>.Info($"Queued narration '{clip.Key}' (priority {priority})");
    }

    public static void Dequeue(string key)
    {
        int indexToRemove = Queue.RemoveAll(p => p.Clip.Key.Contains(key));
    }

    public static void StopPlayingCurrent()
    {
        if (Source != null) Source.Stop();
    }

    public static IEnumerator CycleQueue()
    {
        while (ShipStatus.Instance)
        {
            if (Queue.Count > 0)
            {
                var item = Queue[0];
                Queue.RemoveAt(0);

                if (item.Clip?.Clip != null)
                {
                    PlayClip(item.Clip);
                    SubtitleManager.Instance.StartSubtitle(item.Clip.Subtitle, NarratorRegistry.ActiveNarrator.Name, item.Clip.Clip.length, SubtitleRegistry.ActiveSubtitle);
                    Logger<NarratorModPlugin>.Info($"{item.Clip.Subtitle}");
                    yield return new WaitForSeconds(item.Clip.Clip.length);
                    //SubtitleManager.Instance.StopSubtitle();
                    OnClipFinished?.Invoke(item.Clip);
                }
            }

            yield return null;
        }
    }

    private static void PlayClip(NarrationEntryNormal clip)
    {
        Logger<NarratorModPlugin>.Info($"Playing narration '{clip.Key}'");
        Source = SoundManager.Instance.PlaySound(clip.Clip, false);
        OnClipStarted?.Invoke(clip);
    }

    /// Call this on round start/end so stale queued lines from a previous round don't bleed in.
    public static void ClearQueue() => Queue.Clear();
}