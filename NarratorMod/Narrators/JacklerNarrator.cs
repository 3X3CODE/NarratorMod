using NarratorMod.Extensions;

namespace NarratorMod.Narrators;

public class JacklerNarrator : BaseNarrator
{
    public override string Id { get; } = "com.nm.jackler";
    public override string Name { get; } = "$ir Jackler";
    public override string Author { get; } = "Narrator Mod";
    public override NarratorGenders Gender { get; } = NarratorGenders.Male;
    public override string BundleId { get; } = "narrator-mod";

    public JacklerNarrator()
    {
        AutoGenerateFromBundle();
    }
}