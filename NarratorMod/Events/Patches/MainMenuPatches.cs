using HarmonyLib;
using NarratorMod.API;
using UnityEngine.Events;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class MainMenuPatches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Awake))]
    [HarmonyPostfix]
    public static void ButtonRegistrations(MainMenuManager __instance)
    {
        __instance.inventoryButton.OnClick.AddListener((Action)(() =>
        {
            NarrationEvents.RaiseHoverMenuButton("inventory");
        }));
        
        __instance.myAccountButton.OnClick.AddListener((Action)(() =>
        {
            NarrationEvents.RaiseHoverMenuButton("account");
        }));
        
        __instance.PlayOnlineButton.OnClick.AddListener((Action)(() =>
        {
            NarrationEvents.RaiseHoverMenuButton("online");
        }));
        
        __instance.freePlayButton.OnClick.AddListener((Action)(() =>
        {
            NarrationEvents.RaiseHoverMenuButton("freeplay");
        }));
        
        __instance.playLocalButton.OnClick.AddListener((Action)(() =>
        {
            NarrationEvents.RaiseHoverMenuButton("local");
        }));
    }
}