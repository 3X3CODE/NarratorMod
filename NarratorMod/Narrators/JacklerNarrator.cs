using NarratorMod.Extensions;

namespace NarratorMod.Narrators;

public class JacklerNarrator : BaseNarrator
{
    public override string Id { get; } = "com.nm.jackler";
    public override string Name { get; } = "$ir Jackler";
    public override string Author { get; } = "NarratorMod";

    public JacklerNarrator()
    {
        AutoGenerateEntries("narrator-mod");
    }
}