using NarratorMod.API;
using NarratorMod.Events;
using NarratorMod.Events.Patches;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class ChatConditions : INarrationCondition
{
    public void Subscribe()
    {
        //NarrationEvents.OnSendChatMessage += OnSendMessage;
    }
    
    public void Unsubscribe()
    {
        //NarrationEvents.OnSendChatMessage -= OnSendMessage;
    }

    public void OnSendMessage(PlayerControl p, string text)
    {
        Logger<NarratorModPlugin>.Info($"Player {p.name} said: {text}");
        
        if (MeetingHud.Instance && p == PlayerControl.LocalPlayer)
        {
            MeetingPatches.MessagesSent++;
        }
        
        if (DoesContainText(text, "start")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "go")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "begin")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "setting")) NarratorRegistry.TriggerEvent("");

        if ((DoesContainText(text, "clear") || DoesContainText(text, "safe")) && !DoesContainText(text, "not"))
        {
            if (p == PlayerControl.LocalPlayer)
            {
                Logger<NarratorModPlugin>.Info("You cleared someone");
            }
            else if (DoesContainText(text, PlayerControl.LocalPlayer.name))
            {
                Logger<NarratorModPlugin>.Info("Someone cleared you");
            }
        }
    }
    
    public static bool DoesContainText(string message, string text)
    {
        return message.Contains(text, StringComparison.OrdinalIgnoreCase);
    }
}