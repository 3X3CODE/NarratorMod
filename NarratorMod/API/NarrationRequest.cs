namespace NarratorMod.API;

// unused for now

public readonly struct NarrationRequest
{
    public readonly string Key;
    public readonly int Priority;
    public readonly float Weight; // relative pick chance among same-priority candidates
    public readonly object Context;

    public NarrationRequest(string key, int priority = 0, float weight = 1f, object context = null)
    {
        Key = key;
        Priority = priority;
        Weight = weight <= 0 ? 1f : weight;
        Context = context;
    }
}