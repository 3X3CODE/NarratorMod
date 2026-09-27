using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class GameOverPatches
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    [HarmonyPrefix]
    public static void EndPatch(EndGameResult endGameResult)
    {
        NarrationEvents.RaiseGameEnd(endGameResult.GameOverReason);
    }
}