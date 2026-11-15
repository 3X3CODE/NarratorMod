using HarmonyLib;
using BepInEx.Unity.IL2CPP.Utils;
using NarratorMod.API;
using NarratorMod.Narrators;
using Reactor.Utilities;

namespace NarratorMod.Patches;

[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Begin))]
public class ShipStatusPatch
{
    public static void Postfix(ShipStatus __instance)
    {
        __instance.StartCoroutine(NarrationManager.CycleQueue());
    }
}