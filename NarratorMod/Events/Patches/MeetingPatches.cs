using HarmonyLib;
using InnerNet;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class MeetingPatches
{
    private static float VoteTimer;
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Awake))]
    [HarmonyPostfix]
    public static void OnMeetingOpen()
    {
        
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    [HarmonyPostfix]
    public static void OnMeetingClose()
    {
        
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CastVote))]
    [HarmonyPostfix]
    public static void CastVote(PlayerId srcPlayerId, PlayerId suspectPlayerId)
    {
        NetworkedPlayerInfo playerById = GameData.Instance.GetPlayerById(srcPlayerId);
        NetworkedPlayerInfo playerById2 = GameData.Instance.GetPlayerById(suspectPlayerId);

        if (playerById._object == PlayerControl.LocalPlayer)
        {
            if (VoteTimer < 10f)
            {
                
            }

            if (VoteTimer > 40f)
            {
                
            }
        }
    }
    
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))]
    [HarmonyPostfix]
    public static void OnVotingEnd()
    {
        
    }

    public static void Deteriorate()
    {
        if (MeetingHud.Instance && MeetingHud.Instance.CurrentState == MeetingHud.MeetingStates.NotVoted)
        {
            VoteTimer += Time.deltaTime;
        }
    }
}