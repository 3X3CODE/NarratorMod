using HarmonyLib;
using NarratorMod.Events.Enums;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    public static SystemTypes LastRoom;
    public static bool IsPlayerOnCard;

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcMurderPlayer))]
    [HarmonyPostfix]
    public static void MurderPatch(PlayerControl target, bool didSucceed)
    {
        if (didSucceed)
        {
            NarrationEvents.RaiseImpostorEvent(ImpostorEvents.Kill);
            //Logger<NarratorModPlugin>.Info($"killed someone"); 
        }
    }
    
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.Update))]
    [HarmonyPostfix]
    public static void EnterRoomPatch()
    {
        if (!PlayerControl.LocalPlayer) return;
        Vector3 playerPos = PlayerControl.LocalPlayer.GetTruePosition();
        
        SystemTypes currentRoom = LastRoom;

        if (ShipStatus.Instance)
        {
            foreach (var room in ShipStatus.Instance.AllRooms)
            {
                if (room.roomArea.OverlapPoint(playerPos))
                {
                    currentRoom = room.RoomId;
                }
            }
    
            if (currentRoom != LastRoom)
            {
                LastRoom = currentRoom;
                NarrationEvents.RaiseRoomChangeEvent(LastRoom);
            }
            TaskPatches.Deteriorate();
            MeetingPatches.Deteriorate();
        }
    }
}