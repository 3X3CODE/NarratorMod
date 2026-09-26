using NarratorMod.API;
using UnityEngine;

namespace NarratorMod.Extensions;

public interface INarrator
{
    string Id { get; }
    string Name { get; }
    string Author { get; }
    // string BundleName { get; }
    NarrationEntryDatabase Database { get; }
    NarrationEntry? GetEntry(string clipKey);
}