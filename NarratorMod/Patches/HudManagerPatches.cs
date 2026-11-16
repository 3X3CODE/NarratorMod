using BepInEx.Unity.IL2CPP.Utils;
using HarmonyLib;
using NarratorMod.API;
using NarratorMod.Components.Subtitles;
using UnityEngine;

namespace NarratorMod.Patches;

[HarmonyPatch]
public class HudManagerPatches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    [HarmonyPostfix]
    public static void SubtitleInitializePatch(HudManager __instance)
    {
        GameObject obj = new GameObject("NMSubtitleManager");
        obj.transform.SetParent(__instance.transform);
        obj.AddComponent<SubtitleManager>();
        
        NarrationManager.ClearQueue();
        __instance.StartCoroutine(NarrationManager.CycleQueue());
    }
}