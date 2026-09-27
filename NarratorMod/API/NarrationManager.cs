using System;
using System.Collections;
using System.Collections.Generic;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.API;

public static class NarrationManager
{
    private static AudioSource Source;
    
    private class QueueItem
    {
        public NarrationEntry? Clip;
        public int Priority;
    }

    private static readonly List<QueueItem> Queue = new();

    /// Fired right when a clip starts playing — subscribe here to show captions.
    public static event Action<NarrationEntry>? OnClipStarted;

    /// Fired when a clip finishes playing — subscribe here to hide captions.
    public static event Action<NarrationEntry>? OnClipFinished;

    public static void Enqueue(NarrationEntry clip, int priority = 0)
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
        int indexToRemove = Queue.RemoveAll(p => p.Clip.Key.Value.Contains(key));
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
                    yield return new WaitForSeconds(item.Clip.Clip.Value.length);
                    OnClipFinished?.Invoke(item.Clip);
                }
            }

            yield return null;
        }
    }

    private static void PlayClip(NarrationEntry clip)
    {
        Logger<NarratorModPlugin>.Info($"Playing narration '{clip.Key}'");
        Source = SoundManager.Instance.PlaySound(clip.Clip, false);
        OnClipStarted?.Invoke(clip);
    }

    /// Call this on round start/end so stale queued lines from a previous round don't bleed in.
    public static void ClearQueue() => Queue.Clear();
}