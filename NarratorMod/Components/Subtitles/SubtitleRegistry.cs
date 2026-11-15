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

    public static string SetNext(ISubtitle? subtitle = null)
    {
        subtitle ??= ActiveSubtitle;
        if (subtitle == null) return "";
        int indexOfLast = RegisteredSubtitles.IndexOf(subtitle);
        int indexOfNext = indexOfLast + 1 > RegisteredSubtitles.Count - 1 ? 0 : indexOfLast + 1;
        ISubtitle next = RegisteredSubtitles[indexOfNext];
        SetActiveSubtitle(next);
        return next.GetType().Name;
    }
    
    public static string SetPrevious(ISubtitle? subtitle = null)
    {
        subtitle ??= ActiveSubtitle;
        if (subtitle == null) return "";
        int indexOfLast = RegisteredSubtitles.IndexOf(subtitle);
        int indexOfNext = indexOfLast - 1 < 0 ? RegisteredSubtitles.Count - 1 : indexOfLast - 1;
        ISubtitle next = RegisteredSubtitles[indexOfNext];
        SetActiveSubtitle(next);
        return next.GetType().Name;
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