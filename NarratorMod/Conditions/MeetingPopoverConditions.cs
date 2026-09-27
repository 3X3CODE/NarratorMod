using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class MeetingPopoverConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnMeetingPopOverOpen += OnMeetingPopoverOpen;
        NarrationEvents.OnMeetingPopOverClose += OnMeetingPopoverClose;
    }

    public void OnMeetingPopoverOpen()
    {
        NarratorRegistry.TriggerEvent("");
    }

    public void OnMeetingPopoverClose()
    {
        NarratorRegistry.TriggerEvent("");
    }
}