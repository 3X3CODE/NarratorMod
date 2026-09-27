using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class EnterRoomConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnEnterRoom += OnEnterRoom;
    }

    public void OnEnterRoom(SystemTypes room)
    {
        NarratorRegistry.TriggerEvent($"room.enter.{room.ToString()}");
    }
}