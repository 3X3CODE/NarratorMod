namespace NarratorMod.State;

public class PlayerState
{
    public PlainShipRoom CurrentRoom;
    public float? LastKilledAsVictimTime;   // Time.time when this player was murdered
    public byte? LastKillerId;
}