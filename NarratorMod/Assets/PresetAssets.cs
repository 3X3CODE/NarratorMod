using System.Reflection;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace NarratorMod.Assets;

public class PresetAssets
{
    public static AssetBundle AssetBundle { get; set; } = AssetManager.LoadBundle("narrator-mod");
    public static GameObject CreditsMenu { get; set; } = AssetManager.LoadAsset<GameObject>("CreditsScreen", "narrator-mod").DontDestroy().DontDestroyOnLoad().DontDestroyOnLoad();
    public static GameObject CreditsPullerPrefab { get; set; } = AssetManager.LoadAsset<GameObject>("PullerPrefab", "narrator-mod").DontDestroy().DontDestroyOnLoad().DontDestroyOnLoad();
    public static GameObject RadioPrefab { get; set; } = AssetManager.LoadAsset<GameObject>("RadioPrefab", "narrator-mod").DontDestroy().DontDestroyOnLoad().DontDestroyOnLoad();
}