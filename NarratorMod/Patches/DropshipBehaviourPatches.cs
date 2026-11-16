using HarmonyLib;
using NarratorMod.Assets;
using NarratorMod.Components;
using UnityEngine;

using Object = 
    UnityEngine.Object;

namespace NarratorMod.Patches;

[HarmonyPatch]
public class DropshipBehaviourPatches
{
    public static Vector3 RadioPosition = new(1.5f, 1.65f, -2f); //Available here for mods that change the lobby!
    [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
    [HarmonyPostfix]
    public static void LobbyBehaviour_Start_Postfix(LobbyBehaviour __instance)
    {
        var radio = Object.Instantiate(Assets.PresetAssets.RadioPrefab, __instance.transform);
        radio.transform.position = RadioPosition;
        radio.transform.localScale = new(0.1f, 0.1f, 1);
        radio.AddComponent<RadioConsole>();
        __instance.DropShipSound = AssetManager.LoadAsset<AudioClip>("Lobby Music.mp3");
        __instance.StartCoroutine(__instance.DelayPlayDropshipAmbience());
    }
}