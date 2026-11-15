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
        //NarrationEvents.RaiseMeetingPopOverToggle(true);
        NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenButton);
    }
    
    /*[HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.OnDisable))]
    [HarmonyPostfix]
    public static void ClosePatch()
    {
        NarrationEvents.RaiseMeetingPopOverToggle(false);
    }*/
    
    /*[HarmonyPatch(typeof(EmergencyMinigame), nameof(EmergencyMinigame.CallMeeting))]
    [HarmonyPostfix]
    public static void CallMeetingPatch()
    {
        NarrationEvents.RaiseMeetingEvent(MeetingEvents.Start, null);
    }*/
}