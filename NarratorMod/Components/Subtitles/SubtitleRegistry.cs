using System.Reflection;
using NarratorMod.Extensions;
using Reactor.Utilities;

namespace NarratorMod.Components.Subtitles;

public class SubtitleRegistry
{
    public static List<ISubtitle> RegisteredSubtitles = new();
    public static ISubtitle? ActiveSubtitle;

    public static void RegisterSubtitle(ISubtitle subtitle)
    {
        if (RegisteredSubtitles.Contains(subtitle)) return;
        
        RegisteredSubtitles.Add(subtitle);
        
        Logger<NarratorModPlugin>.Info($"Registered subtitle : {subtitle.GetType().Name}");
        
        if (ActiveSubtitle == null) SetActiveSubtitle(subtitle);
    }

    public static void SetActiveSubtitle(ISubtitle subtitle)
    {
        ActiveSubtitle = subtitle;
        Logger<NarratorModPlugin>.Info($"Set active subtitle : {subtitle.GetType().Name}");
    }

    public static void SetNext(ISubtitle? subtitle = null)
    {
        if (RegisteredSubtitles.Count <= 0) return;
        if (ActiveSubtitle == null)
        {
            SetActiveSubtitle(RegisteredSubtitles[0]);
            return;
        }
        int index = RegisteredSubtitles.IndexOf(ActiveSubtitle);
        if (index != -1 && index + 1 < RegisteredSubtitles.Count)
        {
            SetActiveSubtitle(RegisteredSubtitles[index + 1]);
            return;
        }
    }
    
    public static void SetPrevious(ISubtitle? subtitle = null)
    {
        if (RegisteredSubtitles.Count <= 0) return;
        if (ActiveSubtitle == null)
        {
            SetActiveSubtitle(RegisteredSubtitles[0]);
            return;
        }
        int index = RegisteredSubtitles.IndexOf(ActiveSubtitle);
        if (index > 0)
        {
            SetActiveSubtitle(RegisteredSubtitles[index - 1]);
            return;
        }
    }
    
    public static void RegisterAllFromAssembly(Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        Type baseType = typeof(ISubtitle);

        foreach (var type in assembly.GetTypes())
        {
            if (baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract)
            {
                RegisterSubtitle((ISubtitle)Activator.CreateInstance(type));
            }
        }
    }
}