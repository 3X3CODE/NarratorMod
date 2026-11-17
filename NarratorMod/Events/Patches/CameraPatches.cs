/*using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class CameraPatches
{
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Begin))]
    [HarmonyPostfix]
    public static void CameraOpenPatch(Minigame __instance)
    {
        PlanetSurveillanceMinigame? polusCams = __instance.TryCast<PlanetSurveillanceMinigame>();
        SurveillanceMinigame? skeldAirshipCams = __instance.TryCast<SurveillanceMinigame>();

        if (polusCams != null)
        {
            Logger<NarratorModPlugin>.Info("Cams opened");
        }

        if (skeldAirshipCams != null)
        {
            Logger<NarratorModPlugin>.Info("Cams opened");
        }
    }
    
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Close), new Type[] { })]
    [HarmonyPostfix]
    public static void CameraClosePatch(Minigame __instance)
    {
        PlanetSurveillanceMinigame? polusCams = __instance.TryCast<PlanetSurveillanceMinigame>();
        SurveillanceMinigame? skeldAirshipCams = __instance.TryCast<SurveillanceMinigame>();

        if (polusCams != null)
        {
            Logger<NarratorModPlugin>.Info("Cams closed");
        }

        if (skeldAirshipCams != null)
        {
            Logger<NarratorModPlugin>.Info("Cams closed");
        }
    }
}*/