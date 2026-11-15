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
        //NarrationEvents.RaiseSendChatMessage(sourcePlayer, chatText);
    }
}