using NarratorMod.Events;
using UnityEngine;

namespace NarratorMod.State;

public static class GameStateTracker
{
    public static Dictionary<byte, PlayerState> Players = new();

    public static PlayerState Get(byte id)
    {
        if (!Players.TryGetValue(id, out var state))
            Players[id] = state = new PlayerState();
        return state;
    }

    public static void Reset() => Players.Clear();

    static GameStateTracker()
    {
        NarrationEvents.OnKill += (killer, victim) =>
        {
            var s = Get(victim.PlayerId);
            s.LastKilledAsVictimTime = Time.time;
            s.LastKillerId = killer.PlayerId;
        };

        //NarrationEvents.OnEnterRoom += (room) =>
        //    Get(player.PlayerId).CurrentRoom = room;
    }
}