using NarratorMod.API;
using UnityEngine;

namespace NarratorMod.Extensions;

public interface INarrator
{
    string Id { get; }
    string Name { get; }
    string Author { get; }
    NarratorGenders Gender { get; }
    string BundleId { get; }
    Sprite? ProfileImage { get; }
    AudioClip? TrialAudio { get; }
    
    NarrationEntryDatabase Database { get; }
    NarrationEntryNormal? GetEntry(string clipKey);
}