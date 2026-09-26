using Reactor.Utilities;

namespace NarratorMod.API;

public class NarrationEntryDatabase
{
    private readonly Dictionary<string, NarrationEntry> _entriesByExactKey = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, List<NarrationEntry>> _groupedEntries = new(StringComparer.OrdinalIgnoreCase);
    private readonly System.Random _random = new();

    /// <summary>
    /// Adds a populated entry to the database and builds prefix index.
    /// </summary>
    public void AddEntry(NarrationEntry entry)
    {
        if (entry == null || string.IsNullOrWhiteSpace(entry.Key)) return;

        //string cleanKey = entry.Key.Value.Trim().ToLower();
        string cleanKey = entry.Key.Value;
        _entriesByExactKey[cleanKey] = entry;
        
        Logger<NarratorModPlugin>.Info(cleanKey);

        // Grouping logic: "vent_01" -> group prefix "vent"
        string groupKey = cleanKey;
        int lastUnderscore = cleanKey.LastIndexOf('_');
        if (lastUnderscore > 0)
        {
            groupKey = cleanKey.Substring(0, lastUnderscore);
        }

        if (!_groupedEntries.ContainsKey(groupKey))
        {
            _groupedEntries[groupKey] = new List<NarrationEntry>();
        }
        _groupedEntries[groupKey].Add(entry);
        Logger<NarratorModPlugin>.Info(groupKey);
    }
    
    public NarrationEntry GetEntry(string key)
    {
        Logger<NarratorModPlugin>.Info(key);
        if (string.IsNullOrWhiteSpace(key)) return null;
        string clean = key.ToLower();
        Logger<NarratorModPlugin>.Info(clean);

        // Direct match check
        if (_entriesByExactKey.TryGetValue(clean, out var exactMatch))
        {
            if (exactMatch == null) Logger<NarratorModPlugin>.Info("entry was null");
            return exactMatch;
        }

        // Fall back to prefix group match
        if (_groupedEntries.TryGetValue(clean, out var group) && group.Count > 0)
        {
            return group[_random.Next(group.Count)];
        }

        return null;
    }

    public IReadOnlyCollection<NarrationEntry> GetAllEntries() => _entriesByExactKey.Values;
}