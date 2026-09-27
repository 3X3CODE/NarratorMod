using HarmonyLib;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    public static SystemTypes LastRoom;
    public static bool IsPlayerOnCard;
    
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    [HarmonyPostfix]
    public static void EnterRoomPatch()
    {
        Vector3 playerPos = PlayerControl.LocalPlayer.GetTruePosition();
        
        SystemTypes currentRoom = LastRoom;

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
            NarrationEvents.RaiseEnterRoom(LastRoom);
        }
        
        TaskPatches.Deteriorate();
    }
}