/*using NarratorMod.API;
using NarratorMod.Events;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class MeetingPopoverConditions : INarrationCondition
{
    public void Subscribe()
    {
        //NarrationEvents.OnMeetingPopOverToggle += OnMeetingPopoverOpen;
    }
    
    public void Unsubscribe()
    {
        //NarrationEvents.OnMeetingPopOverToggle -= OnMeetingPopoverOpen;
    }

    public void OnMeetingPopoverOpen(bool open)
    {
        Logger<NarratorModPlugin>.Info($"Meeting popover toggled {open}");
        
        if (open) NarratorRegistry.TriggerEvent("");
        else NarratorRegistry.TriggerEvent("");
    }
}*/