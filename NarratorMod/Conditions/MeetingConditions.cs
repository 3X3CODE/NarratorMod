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
                NarratorRegistry.TriggerEvent($"English.Meeting.FastVote");
                break;
            case MeetingEvents.LateVote:
                NarratorRegistry.TriggerEvent($"English.Meeting.LateVote");
                break;
            case MeetingEvents.NoVote:
                NarratorRegistry.TriggerEvent($"English.Meeting.NoVote");
                break;
            case MeetingEvents.SkipVote:
                NarratorRegistry.TriggerEvent($"English.Meeting.SkipVote");
                break;
            case MeetingEvents.OpenChat:
                NarratorRegistry.TriggerEvent($"English.Meeting.OpenChat");
                break;
            case MeetingEvents.OpenButton:
                NarratorRegistry.TriggerEvent($"English.Meeting.PopoverOpen");
                break;
            case MeetingEvents.VoteCrew:
                NarratorRegistry.TriggerEvent($"English.Meeting.VoteCrewmate");
                break;
            case MeetingEvents.VoteImpostor:
                NarratorRegistry.TriggerEvent($"English.Meeting.VoteImpostor");
                break;
            case MeetingEvents.VotedAsCrew:
                NarratorRegistry.TriggerEvent($"English.Meeting.VotedOutAsCrew");
                break;
            case MeetingEvents.VotedAsImpostor:
                NarratorRegistry.TriggerEvent($"English.Meeting.VotedOutAsImpostor");
                break;
            case MeetingEvents.VotingAlmostDone:
                NarratorRegistry.TriggerEvent($"English.Meeting.VotingAlmostOver");
                break;
        }
    }
}