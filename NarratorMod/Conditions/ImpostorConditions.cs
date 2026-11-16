using NarratorMod.API;
using NarratorMod.Events;
using NarratorMod.Events.Enums;

namespace NarratorMod.Conditions;

public class ImpostorConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnImpostorEvent += OnImpostorEvent;
    }

    public void Unsubscribe()
    {
        NarrationEvents.OnImpostorEvent -= OnImpostorEvent;
    }

    public void OnImpostorEvent(ImpostorEvents impEvent)
    {
        switch (impEvent)
        {
            case ImpostorEvents.Kill:
                NarratorRegistry.TriggerEvent("Impostor.Kill");
                break;
        }
    }
}