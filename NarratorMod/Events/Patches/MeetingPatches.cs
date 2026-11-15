using HarmonyLib;
using InnerNet;
using NarratorMod.Events.Enums;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class MeetingPatches
{
    private static float VoteTimer;
    public static int MessagesSent;
    private static bool didWarnYapping;
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
    [HarmonyPostfix]
    public static void OnMeetingOpen()
    {
        VoteTimer = 0f;
        //NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenChat, null);
    }
    
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Toggle))]
    [HarmonyPostfix]
    public static void OnChatOpen(ChatController __instance)
    {
        //NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenChat, null);
        NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenChat);
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    [HarmonyPostfix]
    public static void OnMeetingClose()
    {
        //NarrationEvents.RaiseMeetingEvent(MeetingEvents.Close, null);
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CastVote))]
    [HarmonyPostfix]
    public static void CastVote(PlayerId srcPlayerId, PlayerId suspectPlayerId)
    {
        Logger<NarratorModPlugin>.Info($"Voted {srcPlayerId}");
        Logger<NarratorModPlugin>.Info($"Voted {suspectPlayerId}");
        
        NetworkedPlayerInfo playerById = GameData.Instance.GetPlayerById(srcPlayerId);
        NetworkedPlayerInfo playerById2 = GameData.Instance.GetPlayerById(suspectPlayerId);

        if (playerById._object == PlayerControl.LocalPlayer)
        {
            if (VoteTimer < 5f)
            {
                // voted too fast
                //NarrationEvents.RaiseMeetingEvent(MeetingEvents.FastVote, playerById2._object);
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.FastVote);
            }
            
            // check vote cast
            
            //NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotePlayer, playerById2._object);

            if (playerById2.Role.TeamType == RoleTeamTypes.Impostor &&
                playerById2.Role.TeamType == RoleTeamTypes.Impostor)
            {
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VoteImpostor);
            }
        }
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))]
    [HarmonyPostfix]
    public static void OnVotingEnd()
    {
        if (!MeetingHud.Instance.DidVote(PlayerControl.LocalPlayer.PlayerId))
        {
            // didn't vote
            //NarrationEvents.RaiseMeetingEvent(MeetingEvents.NoVote, null);
            NarrationEvents.RaiseMeetingEvent(MeetingEvents.NoVote);
        }
    }
    
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
    [HarmonyPostfix]
    public static void ExilePatch(NetworkedPlayerInfo player, bool voteTie, bool wasOverruled)
    {
        if (!voteTie && player._object == PlayerControl.LocalPlayer && !wasOverruled)
        {
            // check imp/crew
            //NarrationEvents.RaiseMeetingEvent(MeetingEvents.Exiled, null);

            if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Crewmate)
            {
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotedAsCrew);
            }
            if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Impostor)
            {
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotedAsImpostor);
            }
        }
    }

    public static void Deteriorate()
    {
        if (MeetingHud.Instance && MeetingHud.Instance.CurrentState == MeetingHud.MeetingStates.NotVoted)
        {
            VoteTimer += Time.deltaTime;
            
            if (VoteTimer > 40f)
            {
                //NarrationEvents.RaiseMeetingEvent(MeetingEvents.SlowVote, null);
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.LateVote);
            }
        }

        /*if (MessagesSent > 20 && !didWarnYapping)
        {
            NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenChat, null);
            didWarnYapping = true;
        }*/
    }
}