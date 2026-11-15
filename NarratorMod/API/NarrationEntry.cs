using Il2CppInterop.Runtime.InteropTypes.Fields;
using UnityEngine;

namespace NarratorMod.API;

public class NarrationEntry : ScriptableObject
{
    public NarrationEntry(IntPtr ptr) : base(ptr) { }
        
    /// <summary>
    /// Key used to trigger or identify this entry (e.g., "vent_01" or "kill").
    /// </summary>
    public Il2CppStringField Key;

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
}