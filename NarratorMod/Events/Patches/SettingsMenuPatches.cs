/*using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class SettingsMenuPatches
{
    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.OnEnable))]
    [HarmonyPostfix]
    public static void OpenSettings()
    {
        NarrationEvents.RaiseViewSettings();
    }

    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.ValueChanged))]
    [HarmonyPostfix]
    public static void ChangeSetting(OptionBehaviour option)
    {
        NarrationEvents.RaiseChangeSetting(option);
    }
}*/