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
        
        if (endGameResult.GameOverReason == GameOverReason.CrewmatesByTask)
        {
            /* task win */
            Logger<NarratorModPlugin>.Info("Task win"); 
        }

        if (endGameResult.GameOverReason == GameOverReason.CrewmatesByVote)
        {
            /* impostors voted out */
            Logger<NarratorModPlugin>.Info("Impostors voted out"); 
        }
        
        if (endGameResult.GameOverReason == GameOverReason.ImpostorDisconnect) 
        {
            /* impostor disconnected */
            Logger<NarratorModPlugin>.Info("Impostors disconnected"); 
        }

        if (endGameResult.GameOverReason == GameOverReason.CrewmateDisconnect)
        {
            /* crew disconnected */
            Logger<NarratorModPlugin>.Info("Crew disconnected"); 
        }

        if (endGameResult.GameOverReason == GameOverReason.ImpostorsByKill)
        {
            /* impostors killed */
            Logger<NarratorModPlugin>.Info("Impostors killed"); 
        }

        if (endGameResult.GameOverReason == GameOverReason.ImpostorsBySabotage)
        {
            /* impostors sabotaged */
            Logger<NarratorModPlugin>.Info("Impostors sabotaged"); 
        }

        if (endGameResult.GameOverReason == GameOverReason.ImpostorsByVote)
        {
            /* crewmates voted out */
            Logger<NarratorModPlugin>.Info("crew voted out"); 
        }
    }
}