using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class MeetingPopoverPatches
{
    [HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.Begin))]
    [HarmonyPostfix]
    public static void OpenPatch()
    {
        Logger<NarratorModPlugin>.Info("Meeting pop-over opened");
    }
    
    [HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.OnDisable))]
    [HarmonyPostfix]
    public static void ClosePatch()
    {
        Logger<NarratorModPlugin>.Info("Meeting pop-over closed");
    }
}