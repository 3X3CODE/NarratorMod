using System.Collections;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.API;

public class NarrationManager
{
    public static List<AudioClip> Queue = new();

    public static void AddToQueue(AudioClip clip)
    {
        if (clip != null)
        {
            Logger<NarratorModPlugin>.Info("Adding clip to queue");
            Queue.Add(clip);
        }
    }

    public static IEnumerator CycleQueue()
    {
        while (ShipStatus.Instance)
        {
            if (Queue.Count > 0)
            {
                Logger<NarratorModPlugin>.Info("Taking clip");
                AudioClip clip = Queue[0];
                if (clip != null)
                {
                    Logger<NarratorModPlugin>.Info("processing clip");
                    ProcessNarration(clip);
                    yield return new WaitForSeconds(clip.length);
                    Logger<NarratorModPlugin>.Info("done");
                }
                Queue.RemoveAt(0);
            }

            yield return null;
        }
    }

    public static void ProcessNarration(AudioClip? clip)
    {
        if (clip == null) return;

        Logger<NarratorModPlugin>.Info("playing clip");
        SoundManager.Instance.PlaySound(clip, false);
    }
}