using NarratorMod.Events;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class SettingsConditions : INarrationCondition
{
    public void Subscribe()
    {
        //NarrationEvents.OnViewSettings += OnViewSettings;
        //NarrationEvents.OnChangeSetting += OnChangeSetting;
    }
    
    public void Unsubscribe()
    {
        //NarrationEvents.OnViewSettings -= OnViewSettings;
        //NarrationEvents.OnChangeSetting -= OnChangeSetting;
    }

    public void OnViewSettings()
    {
        Logger<NarratorModPlugin>.Info($"You opened settings as a {AmongUsClient.Instance.AmHost}");
    }

    public void OnChangeSetting(OptionBehaviour option)
    {
        Logger<NarratorModPlugin>.Info("setting changed");
    }
}