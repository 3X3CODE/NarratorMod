namespace NarratorMod.Events;

public static class NarrationEvents
{
    public static event Action<PlayerControl, PlayerControl> OnKill; // killer, victim
    public static event Action<PlayerControl, byte> OnReportBody;    // reporter, victimId
    public static event Action OnMeetingStart;
    public static event Action OnMeetingPopOverOpen;
    public static event Action OnMeetingPopOverClose;
    public static event Action<PlayerControl, string> OnSendChatMessage;
    public static event Action OnViewSettings;
    public static event Action<PlayerControl> OnChangeCosmetics;
    public static event Action<OptionBehaviour> OnChangeSetting;
    public static event Action<string> OnHoverMenuButton;
    public static event Action<SystemTypes> OnEnterRoom;
    public static event Action<float> OnCompleteTask;
    public static event Action<GameOverReason> OnGameEnd;
    public static event Action<PlayerControl, PlayerControl> OnPlayerVote;

    public static void RaiseKill(PlayerControl killer, PlayerControl victim) => OnKill?.Invoke(killer, victim);
    public static void RaiseReport(PlayerControl reporter, byte victimId) => OnReportBody?.Invoke(reporter, victimId);
    public static void RaiseMeetingStart() => OnMeetingStart?.Invoke();
    public static void RaiseMeetingPopOverOpen() => OnMeetingPopOverOpen?.Invoke();
    public static void RaiseMeetingPopOverClose() => OnMeetingPopOverClose?.Invoke();
    public static void RaiseRoomChanged(SystemTypes type) => OnEnterRoom?.Invoke(type);
    public static void RaiseSendChatMessage(PlayerControl p, string str) => OnSendChatMessage?.Invoke(p, str);
    public static void RaiseViewSettings() => OnViewSettings?.Invoke();
    public static void RaiseChangeCosmetics(PlayerControl p) => OnChangeCosmetics?.Invoke(p);
    public static void RaiseChangeSetting(OptionBehaviour option) => OnChangeSetting?.Invoke(option);
    public static void RaiseHoverMenuButton(string str) => OnHoverMenuButton?.Invoke(str);
    public static void RaiseEnterRoom(SystemTypes type) => OnEnterRoom?.Invoke(type);
    public static void RaiseCompleteTask(float f) => OnCompleteTask?.Invoke(f);
    public static void RaiseGameEnd(GameOverReason reason) => OnGameEnd?.Invoke(reason);
    public static void RaisePlayerVote(PlayerControl p1, PlayerControl p2) => OnPlayerVote?.Invoke(p1, p2);
}