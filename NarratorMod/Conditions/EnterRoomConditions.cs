using NarratorMod.API;
using NarratorMod.Events;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class EnterRoomConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnRoomChangeEvent += OnEnterRoom;
    }
    
    public void Unsubscribe()
    {
        NarrationEvents.OnRoomChangeEvent -= OnEnterRoom;
    }

    public void OnEnterRoom(SystemTypes room)
    {
        NarratorRegistry.TriggerEvent($"RoomEnter.{room.ToString()}");
        Logger<NarratorModPlugin>.Info($"Room entered {room.ToString()}");
    }
}