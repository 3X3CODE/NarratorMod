using NarratorMod.API;
using NarratorMod.Events;
using NarratorMod.Events.Enums;

namespace NarratorMod.Conditions;

public class TaskConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnCrewmateEvent += OnCrewmateEvent;
    }
    
    public void Unsubscribe()
    {
        NarrationEvents.OnCrewmateEvent -= OnCrewmateEvent;
    }
    
    public void OnCrewmateEvent(CrewmateEvents crewEvent)
    {
        switch (crewEvent)
        {
            case CrewmateEvents.CompleteAllTasks:
                NarratorRegistry.TriggerEvent("Crewmate.CompleteTasks");
                break;
            case CrewmateEvents.FastTask:
                NarratorRegistry.TriggerEvent("Crewmate.FastTask");
                break;
            case CrewmateEvents.LongTask:
                NarratorRegistry.TriggerEvent("Crewmate.LongTask");
                break;
            case CrewmateEvents.NoTask:
                NarratorRegistry.TriggerEvent("Crewmate.NoTaskDone");
                break;
        }
    }
}