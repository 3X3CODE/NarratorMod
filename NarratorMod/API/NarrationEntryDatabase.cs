using Reactor.Utilities;

namespace NarratorMod.API;

public class NarrationEntryDatabase
{
    private readonly Dictionary<string, NarrationEntryNormal> _entriesByExactKey = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, List<NarrationEntryNormal>> _groupedEntries = new(StringComparer.OrdinalIgnoreCase);
    private readonly Random _random = new();

    /// <summary>
    /// Adds a populated entry to the database and builds prefix index.
    /// </summary>
    public void AddEntry(NarrationEntryNormal entry)
    {
        if (entry == null || string.IsNullOrWhiteSpace(entry.Key)) return;
        
        string cleanKey = entry.Key;
        _entriesByExactKey[cleanKey] = entry;
        
        //Logger<NarratorModPlugin>.Info(cleanKey);
        
        string groupKey = cleanKey;
        int lastUnderscore = cleanKey.LastIndexOf('.');
        if (lastUnderscore > 0)
        {
            groupKey = cleanKey.Substring(0, lastUnderscore);
        }

        if (!_groupedEntries.ContainsKey(groupKey))
        {
            _groupedEntries[groupKey] = new List<NarrationEntryNormal>();
        }
        _groupedEntries[groupKey].Add(entry);
        //Logger<NarratorModPlugin>.Info(groupKey);
    }

    public NarrationEntryNormal GetEntry(string key)
    {
        //Logger<NarratorModPlugin>.Info(key);
        if (string.IsNullOrWhiteSpace(key)) return null;
        string clean = $"{NarratorRegistry.ActiveLanguage}.{key.ToLower()}";

        //Logger<NarratorModPlugin>.Info(clean);
        
        // Direct match check
        if (_entriesByExactKey.TryGetValue(clean, out var exactMatch))
        {
            if (exactMatch == null)
            {
                Logger<NarratorModPlugin>.Info("exact entry was null");
            }

            if (exactMatch.ContainsSwearWords != NarratorRegistry.AllowSwear) return null;
            return exactMatch;
        }

        // Fall back to prefix group match
        if (_groupedEntries.TryGetValue(clean, out var group) && group.Count > 0)
        {
            List<NarrationEntryNormal> filteredGroupList =
                group.Where(item => item.ContainsSwearWords == NarratorRegistry.AllowSwear).ToList();
            return filteredGroupList[_random.Next(group.Count)];
        }

        return null;
    }

    public IReadOnlyCollection<NarrationEntryNormal> GetAllEntries() => _entriesByExactKey.Values;
}