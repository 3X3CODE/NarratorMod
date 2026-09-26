using HarmonyLib;
using Reactor.Utilities;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class ChatPatches
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.AddChat))]
    [HarmonyPrefix]
    public static void CheckChatData(PlayerControl sourcePlayer, string chatText)
    {
        Logger<NarratorModPlugin>.Info(chatText);
        
        NarrationEvents.RaiseSendChatMessage(sourcePlayer, chatText);
        
        if (DoesContainText(chatText, "start"))
        {
            Logger<NarratorModPlugin>.Info($"{sourcePlayer.name} said {chatText}"); 
            
        }

        if (DoesContainText(chatText, "go"))
        {
            Logger<NarratorModPlugin>.Info($"{sourcePlayer.name} said {chatText}"); 
        }

        if (DoesContainText(chatText, "begin"))
        {
            Logger<NarratorModPlugin>.Info($"{sourcePlayer.name} said {chatText}"); 
        }

        if (DoesContainText(chatText, "setting"))
        {
            Logger<NarratorModPlugin>.Info($"{sourcePlayer.name} said {chatText}"); 
        }

        if (DoesContainText(chatText, "change"))
        {
            Logger<NarratorModPlugin>.Info($"{sourcePlayer.name} said {chatText}"); 
        }
    }

    public static bool DoesContainText(string message, string text)
    {
        return message.Contains(text, StringComparison.OrdinalIgnoreCase);
    }
}