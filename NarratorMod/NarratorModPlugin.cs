using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using NarratorMod.API;
using NarratorMod.Assets;
using NarratorMod.Narrators;
using Reactor;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod;

[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInPlugin(Id, Name, Version)]
public class NarratorModPlugin : BasePlugin
{
    public const string Id = "com.exec.narratormod";
    public const string Name = "Narrator Mod";
    public const string Version = "1.0.0-dev";

    public static bool IsDevBuild => Version.Contains("dev");

    public static Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        ClassInjector.RegisterTypeInIl2Cpp<NarrationEntry>();
        
        Harmony.PatchAll();
        ReactorCredits.Register(Name, Version, IsDevBuild, ReactorCredits.AlwaysShow);
        
        AssetBundle bundle = AssetManager.LoadBundle("narrator-mod");
        foreach (var asset in bundle.GetAllAssetNames())
        {
            Logger<NarratorModPlugin>.Info(asset);
        }
        
        NarratorRegistry.RegisterNarrator(new JacklerNarrator());
    }
}
