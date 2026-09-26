using Il2CppInterop.Runtime.InteropTypes.Fields;
using UnityEngine;

namespace NarratorMod.API;

public class NarrationEntry(IntPtr ptr) : ScriptableObject(ptr)
{
    /// <summary>
    /// Key used to trigger or identify this entry (e.g., "vent_01" or "kill").
    /// </summary>
    public Il2CppStringField Key;

    /// <summary>
    /// Exact name of the audio clip asset inside the AssetBundle or source directory.
    /// </summary>
    public Il2CppStringField ClipName;

    /// <summary>
    /// Subtitle or caption text displayed on screen while the clip plays.
    /// </summary>
    public Il2CppStringField Subtitle;

    /// <summary>
    /// Language code following ISO format (e.g., "en-US", "es-ES").
    /// </summary>
    public Il2CppStringField Language;

    /// <summary>
    /// Indicates if this clip contains strong language/profanity.
    /// </summary>
    public Il2CppValueField<bool> ContainsSwearWords;

    /// <summary>
    /// Direct reference to loaded Unity AudioClip (populated when loaded).
    /// </summary>
    public Il2CppReferenceField<AudioClip> Clip;

    /// <summary>
    /// Length of the audio clip in seconds. Returns 0 if clip is not loaded.
    /// </summary>
    //public Il2CppValueField<float> Duration => Clip != null ? Clip.Value.length : 0f;

    /*public NarrationEntry(string key, string clipName, string subtitle, string language, bool containsSwearWords)
    {
        Key = key;
        ClipName = clipName;
        Subtitle = subtitle;
        Language = language;
        ContainsSwearWords = containsSwearWords;
    }*/
}