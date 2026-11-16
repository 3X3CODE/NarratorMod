using NarratorMod.API;
using NarratorMod.Events;
using NarratorMod.Events.Enums;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class GameOverConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnGameOverEvent += OnGameOver;
    }
    
    public void Unsubscribe()
    {
        NarrationEvents.OnGameOverEvent -= OnGameOver;
    }

    public void OnGameOver(GameOverEvents reason)
    {
        Logger<NarratorModPlugin>.Info($"Game over: {reason.ToString()}");
        
        switch (reason)
        {
            case GameOverEvents.CrewWin:
                NarratorRegistry.TriggerEvent($"GameOver.Crewmate.CrewWin");
                break;
            case GameOverEvents.CrewTaskWin:
                // doesnt work
                NarratorRegistry.TriggerEvent($"GameOver.Crewmate.CrewTaskWin");
                break;
            case GameOverEvents.CrewLoss:
                NarratorRegistry.TriggerEvent($"GameOver.Crewmate.CrewLoss");
                break;
            case GameOverEvents.ImpWin:
                NarratorRegistry.TriggerEvent($"GameOver.Impostor.ImpostorWin");
                break;
            case GameOverEvents.ImpTaskWin:
                NarratorRegistry.TriggerEvent($"GameOver.Impostor.ImpostorTaskWin");
                break;
            case GameOverEvents.ImpLoss:
                NarratorRegistry.TriggerEvent($"GameOver.Impostor.ImpostorLoss");
                break;
        }
    }
}