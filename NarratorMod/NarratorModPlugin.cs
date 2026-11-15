using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using NarratorMod.API;
using NarratorMod.Assets;
using NarratorMod.Components.Subtitles;
using NarratorMod.Conditions;
using NarratorMod.Narrators;
using Reactor;
using Reactor.Utilities;

// welcome to narrator mod version 3

// im having an aneurism <3

namespace NarratorMod;

[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInPlugin(Id, Name, Version)]
public class NarratorModPlugin : BasePlugin
{
    public const string Id = "com.exec.narratormod";
    public const string Name = "Narrator Mod";
    public const string Version = "1.0.0-dev";

    public static string LanguageOption;
    public static bool SwearOption;

    public static bool IsDevBuild => Version.Contains("dev");

    public static Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        ClassInjector.RegisterTypeInIl2Cpp<NarrationEntry>();
        
        Harmony.PatchAll();
        AssetManager.LoadBundle("narrator-mod");
        NarratorRegistry.RegisterNarrator(new JacklerNarrator());
        ConditionManager.RegisterAllFromAssembly();
        SubtitleRegistry.RegisterAllFromAssembly();
        ReactorCredits.Register(Name, Version, IsDevBuild, ReactorCredits.AlwaysShow);
        
        Log.LogMessage($"{Name} {Version} loaded successfully");
    }
}
