using NarratorMod.Extensions;

namespace NarratorMod.Narrators;

// made strictly for the test of extension narrators and the settings menu

public class TestNarrator : BaseNarrator
{
    public override string Id { get; } = "com.nm.test";
    public override string Name { get; } = "Test Narrator";
    public override string Author { get; } = "Tung Tung Sahur";
    public override NarratorGenders Gender { get; } = NarratorGenders.Female;
    public override string BundleId { get; } = "test-bundle";

    public TestNarrator()
    {
        AutoGenerateFromBundle();
    }
}