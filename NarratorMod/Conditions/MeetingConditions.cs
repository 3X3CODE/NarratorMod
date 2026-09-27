using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class MeetingConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnMeetingStart += OnMeetingStart;
    }
    
    public void OnMeetingStart()
    {
        NarratorRegistry.TriggerEvent("");
    }
}