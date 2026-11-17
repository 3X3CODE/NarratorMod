using HarmonyLib;
using NarratorMod.Events.Enums;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class MeetingPopoverPatches
{
    [HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.Begin))]
    [HarmonyPostfix]
    public static void OpenPatch()
    {
        //Logger<NarratorModPlugin>.Info($"button opened");
        NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenButton);
    }
}