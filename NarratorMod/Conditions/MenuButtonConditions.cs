using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class MenuButtonConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnHoverMenuButton += OnHoverButton;
    }
    
    public void OnHoverButton(string button)
    {
        NarratorRegistry.TriggerEvent("");
    }
}