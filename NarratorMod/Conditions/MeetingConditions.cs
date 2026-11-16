using NarratorMod.API;
using NarratorMod.Events;
using NarratorMod.Events.Enums;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class MeetingConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnMeetingEvent += OnMeetingEvent;
    }
    
    public void Unsubscribe()
    {
        NarrationEvents.OnMeetingEvent -= OnMeetingEvent;
    }
    
    public void OnMeetingEvent(MeetingEvents meetingEvent)
    {
        switch (meetingEvent)
        {
            case MeetingEvents.FastVote:
                NarratorRegistry.TriggerEvent($"Meeting.FastVote");
                break;
            case MeetingEvents.LateVote:
                NarratorRegistry.TriggerEvent($"Meeting.LateVote");
                break;
            case MeetingEvents.NoVote:
                NarratorRegistry.TriggerEvent($"Meeting.NoVote");
                break;
            case MeetingEvents.SkipVote:
                NarratorRegistry.TriggerEvent($"Meeting.SkipVote");
                break;
            case MeetingEvents.OpenChat:
                NarratorRegistry.TriggerEvent($"Meeting.OpenChat");
                break;
            case MeetingEvents.OpenButton:
                NarratorRegistry.TriggerEvent($"Meeting.PopoverOpen");
                break;
            case MeetingEvents.VoteCrew:
                NarratorRegistry.TriggerEvent($"Meeting.VoteCrewmate");
                break;
            case MeetingEvents.VoteImpostor:
                NarratorRegistry.TriggerEvent($"Meeting.VoteImpostor");
                break;
            case MeetingEvents.VotedAsCrew:
                NarratorRegistry.TriggerEvent($"Meeting.VotedOutAsCrew");
                break;
            case MeetingEvents.VotedAsImpostor:
                NarratorRegistry.TriggerEvent($"Meeting.VotedOutAsImpostor");
                break;
            case MeetingEvents.VotingAlmostDone:
                NarratorRegistry.TriggerEvent($"Meeting.VotingAlmostOver");
                break;
        }
    }
}