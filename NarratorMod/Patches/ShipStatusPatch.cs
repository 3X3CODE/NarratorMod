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
        //NarratorRegistry.RegisterNarrator(new JacklerNarrator());
        
        __instance.StartCoroutine(NarrationManager.CycleQueue());
        NarratorRegistry.TriggerEvent("electrical");
        
        Logger<NarratorModPlugin>.Info("Clip triggered");
    }
}