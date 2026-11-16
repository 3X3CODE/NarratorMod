/*using NarratorMod.API;
using NarratorMod.Events;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class MenuButtonConditions : INarrationCondition
{
    public void Subscribe()
    {
       // NarrationEvents.OnHoverMenuButton += OnHoverButton;
    }
    
    public void Unsubscribe()
    {
        //NarrationEvents.OnHoverMenuButton -= OnHoverButton;
    }
    
    public void OnHoverButton(string button)
    {
        NarratorRegistry.TriggerEvent("");
        
        Logger<NarratorModPlugin>.Info($"Menu button clicked: {button}");
    }
}*/