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
    public abstract NarratorGenders Gender { get; }
    public abstract string BundleId { get; }
    public Sprite? ProfileImage { get; private set; }

    public NarrationEntryDatabase Database { get; } = new();

    public NarrationEntryNormal? GetEntry(string eventKey)
    {
        return Database.GetEntry(eventKey);
    }
    
    public void AutoGenerateFromBundle()
    {
        Logger<NarratorModPlugin>.Info($"Auto generating entries from bundle : {BundleId}");
        
        AssetBundle? bundle = AssetManager.LoadBundle(BundleId);
        if (bundle == null)
        {
            Logger<NarratorModPlugin>.Error($"Failed to load AssetBundle: {BundleId}");
            return;
        }

        foreach (var assetPath in bundle.GetAllAssetNames())
        {
            //Logger<NarratorModPlugin>.Info($"{assetPath}");
            var clip = bundle.LoadAsset<NarrationEntry>(assetPath);
            
            string cleanPath = assetPath.Replace('\\', '/').ToLowerInvariant();
            string[] segments = cleanPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (segments[1] == "narrators" && segments[2] == Id)
            {
                if (segments[^1] == "profile-image.png")
                {
                    var image = bundle.LoadAsset<Sprite>(assetPath);
                    if (image != null)
                    {
                        ProfileImage = image.DontUnload();
                        continue;
                    }
                }
                
                NarratorRegistry.AddLanguage(segments[3]);

                if (clip == null) continue;
                
                NarrationEntryNormal newClip = new(GenerateKey(segments), clip.Subtitle.Value, "", clip.ContainsSwearWords.Value, clip.Clip.Value.DontUnload());
                
                Database.AddEntry(newClip);
            }
        }
        
        Logger<NarratorModPlugin>.Info($"Autogeneration complete");
    }
    
    private string GenerateKey(string[] segments)
    {
        //Logger<NarratorModPlugin>.Info($"Generating key...");
        
        if (segments.Length < 4)
        {
            // return file name
            //Logger<NarratorModPlugin>.Info("Returning file name");
            return Path.GetFileNameWithoutExtension(segments[^1]);
        }
        
        int keySegmentCount = segments.Length - 3;
        string[] keySegments = new string[keySegmentCount];
        
        Array.Copy(segments, 3, keySegments, 0, keySegmentCount);
        
        keySegments[^1] = Path.GetFileNameWithoutExtension(keySegments[^1]);

        string key = string.Join(".", keySegments);
        
        //Logger<NarratorModPlugin>.Info($"Clip generated: {key}");

        return key;
    }
}