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
        NarrationEvents.RaiseMeetingPopOverOpen();
    }
    
    [HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.OnDisable))]
    [HarmonyPostfix]
    public static void ClosePatch()
    {
        NarrationEvents.RaiseMeetingPopOverClose();
    }
    
    [HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.CallMeeting))]
    [HarmonyPostfix]
    public static void CallMeetingPatch()
    {
        NarrationEvents.RaiseMeetingStart();
    }
}