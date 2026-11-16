using HarmonyLib;
using NarratorMod.Events.Enums;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class GameOverPatches
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    [HarmonyPostfix]
    public static void EndPatch(EndGameResult endGameResult)
    {
        //NarrationEvents.RaiseGameEnd(endGameResult.GameOverReason);
        Logger<NarratorModPlugin>.Info($"Game over {endGameResult.GameOverReason}");
        switch (endGameResult.GameOverReason)
        {
            case GameOverReason.CrewmatesByVote:
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.CrewWin);
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.ImpLoss);
                break;
            case GameOverReason.CrewmatesByTask:
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.CrewTaskWin);
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.ImpTaskWin);
                break;
            case GameOverReason.ImpostorsByKill:
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.CrewLoss);
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.ImpWin);
                break;
            case GameOverReason.ImpostorsByVote:
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.CrewLoss);
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.ImpWin);
                break;
            case GameOverReason.ImpostorsBySabotage:
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.CrewLoss);
                NarrationEvents.RaiseGameOverEvent(GameOverEvents.ImpWin);
                break;
        }
    }
}