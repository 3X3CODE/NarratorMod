using NarratorMod.API;
using NarratorMod.Events;

namespace NarratorMod.Conditions;

public class ChatConditions : INarrationCondition
{
    public void Subscribe()
    {
        NarrationEvents.OnSendChatMessage += OnSendMessage;
    }

    public void OnSendMessage(PlayerControl p, string text)
    {
        if (DoesContainText(text, "start")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "go")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "begin")) NarratorRegistry.TriggerEvent("");
        if (DoesContainText(text, "setting")) NarratorRegistry.TriggerEvent("");
    }
    
    public static bool DoesContainText(string message, string text)
    {
        return message.Contains(text, StringComparison.OrdinalIgnoreCase);
    }
}