using AmongUs.GameOptions;
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
    private static float TotalElapsed;
    public static int MessagesSent;
    private static bool didWarnYapping;
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
    [HarmonyPostfix]
    public static void OnMeetingOpen()
    {
        VoteTimer = 0f;
        TotalElapsed = 0f;
    }
    
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Toggle))]
    [HarmonyPostfix]
    public static void OnChatOpen(ChatController __instance)
    {
        if (__instance.IsClosedOrClosing) return;
        NarrationEvents.RaiseMeetingEvent(MeetingEvents.OpenChat);
        //Logger<NarratorModPlugin>.Info($"chat opened"); 
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
        NetworkedPlayerInfo playerById = GameData.Instance.GetPlayerById(srcPlayerId);

        if (playerById._object == PlayerControl.LocalPlayer)
        {
            if (VoteTimer < 5f)
            {
                //Logger<NarratorModPlugin>.Info($"fast vote"); // works
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.FastVote);
            }
            else if(TotalElapsed > GameOptionsManager.Instance.CurrentGameOptions.GetInt(Int32OptionNames.VotingTime) - 5)
            {
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.LateVote);
                //Logger<NarratorModPlugin>.Info($"late vote");
            }
            
            if (suspectPlayerId == 253)
            { 
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.SkipVote);
                //Logger<NarratorModPlugin>.Info($"skipped"); //works
                return;
            }
            
            NetworkedPlayerInfo playerById2 = GameData.Instance.GetPlayerById(suspectPlayerId);

            if (playerById.Role.TeamType == RoleTeamTypes.Impostor &&
                playerById2.Role.TeamType == RoleTeamTypes.Impostor)
            {
                //Logger<NarratorModPlugin>.Info($"voted impostor"); //works
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VoteImpostor);
            }
        }
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CalculateVotes))]
    [HarmonyPostfix]
    public static void OnVotingEnd()
    {
        if (!MeetingHud.Instance.DidVote(PlayerControl.LocalPlayer.PlayerId))
        {
            //Logger<NarratorModPlugin>.Info($"no vote"); //didnt work
            NarrationEvents.RaiseMeetingEvent(MeetingEvents.NoVote);
        }
    }
    
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
    [HarmonyPostfix]
    public static void ExilePatch(NetworkedPlayerInfo player, bool voteTie, bool wasOverruled)
    {
        if (!voteTie && player._object == PlayerControl.LocalPlayer && !wasOverruled)
        {
            if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Crewmate)
            {
                //Logger<NarratorModPlugin>.Info($"voted as crew"); //works
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotedAsCrew);
            }
            if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Impostor)
            {
                //Logger<NarratorModPlugin>.Info($"voted as impostor"); //works
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotedAsImpostor);
            }
        }
    }

    public static void Deteriorate()
    {
        if (MeetingHud.Instance && MeetingHud.Instance.CurrentState == MeetingHud.MeetingStates.NotVoted)
        {
            VoteTimer += Time.deltaTime;
            TotalElapsed += Time.deltaTime;
            
            if (VoteTimer > 40f)
            {
                NarrationEvents.RaiseMeetingEvent(MeetingEvents.VotingAlmostDone);
                //Logger<NarratorModPlugin>.Info($"almost done voting"); //works
                VoteTimer = 0f;
            }
        }
    }
}