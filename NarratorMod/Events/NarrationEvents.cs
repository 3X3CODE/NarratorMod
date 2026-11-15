using NarratorMod.Events.Enums;

namespace NarratorMod.Events;

public static class NarrationEvents
{
    public static event Action<ImpostorEvents>? OnImpostorEvent; 
    public static event Action<CrewmateEvents>? OnCrewmateEvent; 
    public static event Action<GameOverEvents>? OnGameOverEvent; 
    public static event Action<MeetingEvents>? OnMeetingEvent;
    public static event Action<SystemTypes>? OnRoomChangeEvent;

    public static void RaiseImpostorEvent(ImpostorEvents impEvent) => OnImpostorEvent?.Invoke(impEvent);
    public static void RaiseCrewmateEvent(CrewmateEvents crewEvent) => OnCrewmateEvent?.Invoke(crewEvent);
    public static void RaiseGameOverEvent(GameOverEvents overEvent) => OnGameOverEvent?.Invoke(overEvent);
    public static void RaiseMeetingEvent(MeetingEvents meetingEvent) => OnMeetingEvent?.Invoke(meetingEvent);
    public static void RaiseRoomChangeEvent(SystemTypes room) => OnRoomChangeEvent?.Invoke(room);
}