using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class EnterRoomConditions : INarrationConditions
{
    public void Subscribe()
    {
        NarrationEvents.OnEnterRoom += OnEnterRoom;
    }

    public void OnEnterRoom(SystemTypes room)
    {
        NarratorRegistry.TriggerEvent("");
    }
}