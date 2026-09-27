using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class SettingsConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnViewSettings += OnViewSettings;
        NarrationEvents.OnChangeSetting += OnChangeSetting;
    }

    public void OnViewSettings()
    {
        
    }

    public void OnChangeSetting(OptionBehaviour option)
    {
        
    }
}