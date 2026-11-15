using Il2CppInterop.Runtime.InteropTypes.Fields;
using UnityEngine;

namespace NarratorMod.API;

public class NarrationEntryNormal
{
    /// <summary>
    /// Key used to trigger or identify this entry (e.g., "vent_01" or "kill").
    /// </summary>
    public string Key;

    /// <summary>
    /// Subtitle or caption text displayed on screen while the clip plays.
    /// </summary>
    public string Subtitle;

    /// <summary>
    /// Language code following ISO format (e.g., "en-US", "es-ES").
    /// </summary>
    public string Language;

    /// <summary>
    /// Indicates if this clip contains strong language/profanity.
    /// </summary>
    public bool ContainsSwearWords;

    /// <summary>
    /// Direct reference to loaded Unity AudioClip (populated when loaded).
    /// </summary>
    public AudioClip Clip;

    public NarrationEntryNormal(string key, string subtitle, string language, bool containsSwears, AudioClip clip)
    {
        Key = key;
        Subtitle = subtitle;
        Language = language;
        ContainsSwearWords = containsSwears;
        Clip = clip;
    }
}