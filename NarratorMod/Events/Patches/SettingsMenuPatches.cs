using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class SettingsMenuPatches
{
    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.OnEnable))]
    [HarmonyPostfix]
    public static void OpenSettings()
    {
        if (!AmongUsClient.Instance.AmHost)
        {
            // non host views settings
            Logger<NarratorModPlugin>.Info("Non host player is viewing settings"); 
        }
        else
        {
            // host
            Logger<NarratorModPlugin>.Info("Host is viewing settings"); 
        }
    }
}