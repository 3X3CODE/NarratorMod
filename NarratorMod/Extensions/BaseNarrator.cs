using System.Reflection;
using NarratorMod.API;
using NarratorMod.Assets;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace NarratorMod.Extensions;

public abstract class BaseNarrator : INarrator
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract string Author { get; }
    
    public NarrationEntryDatabase Database { get; } = new();

    public NarrationEntry? GetEntry(string eventKey)
    {
        return Database.GetEntry(eventKey);
    }
    
    public void AutoGenerateEntries(string bundleId)
    {
        AssetBundle? bundle = AssetManager.LoadBundle(bundleId);
        if (bundle == null)
        {
            Debug.LogError($"[NarratorMod] Failed to load AssetBundle: {bundleId}");
            return;
        }

        foreach (var assetPath in bundle.GetAllAssetNames())
        {
            var clip = bundle.LoadAsset<NarrationEntry>(assetPath);
            if (clip == null) continue;

            string rawFileName = Path.GetFileNameWithoutExtension(assetPath).ToLower();

            // Auto-populate NarratorEntry using file conventions
            /*var entry = new NarrationEntry
            {
                Key = rawFileName,
                ClipName = assetPath,
                Clip = clip,
                Language = "English",
                ContainsSwearWords = rawFileName.Contains("explicit") || rawFileName.Contains("swear"),
                Subtitle = $"[Playing {rawFileName}]" // Default caption fallback
            };*/
            Logger<NarratorModPlugin>.Info($"Searching for clip {clip.ClipName}");
            AudioClip audioClip = bundle.LoadAsset<AudioClip>(clip.ClipName);
            if (audioClip != null)
            {
                audioClip.DontUnload();
                clip.Clip.Value = audioClip;
                clip.DontUnload();
            }
            else Logger<NarratorModPlugin>.Info($"AudioClip null");
            
            Logger<NarratorModPlugin>.Info($"Adding");
            
            Database.AddEntry(clip);
            
            Logger<NarratorModPlugin>.Info($"Added to database");
        }
    }
}