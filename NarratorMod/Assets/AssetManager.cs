using System.Reflection;
using NarratorMod.API;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace NarratorMod.Assets;

public class AssetManager
{
    public static Dictionary<string, AssetBundle> LoadedBundles = new();

    public static AssetBundle? LoadBundle(string name, Assembly? assembly = null)
    {
        if (LoadedBundles.TryGetValue(name, out var assetBundle))
        {
            if (assetBundle != null) return assetBundle;
        }
        
        assembly ??= Assembly.GetCallingAssembly();
        AssetBundle bundle = AssetBundleManager.Load(assembly, name);
        //bundle.Unload(false);
        LoadedBundles[name] = bundle;

        return bundle;
    }

    public static T? LoadAsset<T>(string assetName, string bundleId = "") where T : UnityEngine.Object
    {
        if (bundleId == String.Empty)
        {
            foreach (var bundle in LoadedBundles.Values)
            {
                if (bundle.Contains(assetName))
                    return bundle.LoadAsset<T>(assetName);
            }
        }
        
        else if (!LoadedBundles.ContainsKey(bundleId)) return null;
        
        return LoadedBundles[bundleId].LoadAsset<T>(assetName);
    }
    
    public static Texture2D GetTexture(string name, string bundleId)
    {
        Texture2D tex = LoadAsset<Texture2D>(name, bundleId);
        
        if (tex != null)
        {
            tex.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        return tex;
    }
}