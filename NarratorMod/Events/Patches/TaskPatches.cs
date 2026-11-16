using HarmonyLib;
using NarratorMod.API;
using NarratorMod.Events.Enums;
using Reactor.Utilities;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class TaskPatches
{
    private static float TimeSinceLast;
    private static float TotalElapsed;
    private static float IdleTime;
    private static bool IsDoingTask;
    
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Begin))]
    [HarmonyPostfix]
    public static void OnTaskStart(Minigame __instance)
    {
        if (__instance is EmergencyMinigame || __instance is TaskAdderGame) return;
        
        IsDoingTask = true;
        TimeSinceLast = 0f;
        TotalElapsed = 0f;
    }
    
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Close), new Type[0])]
    [HarmonyPostfix]
    public static void OnTaskClose(Minigame __instance)
    {
        if (__instance is TaskAdderGame) return;
        if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Impostor) return;
        
        if (IsDoingTask && TotalElapsed < 10f)
        {
            // took too short on task
            Logger<NarratorModPlugin>.Info($"fast task");
            NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.FastTask);
        }

        IsDoingTask = false;
        IdleTime = 0f;
        TimeSinceLast = 0f;
        TotalElapsed = 0f;

        if (Minigame.Instance is ProcessDataMinigame downloadMinigame)
        {
            NarrationManager.Dequeue("");
            NarrationManager.StopPlayingCurrent();
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
    [HarmonyPostfix]
    public static void OnTaskComplete()
    {
        if (PlayerControl.LocalPlayer.AllTasksCompleted())
        {
            // all tasks completed
            Logger<NarratorModPlugin>.Info($"all tasks done");
            NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.CompleteAllTasks);
        }
    }

    public static void Deteriorate()
    {
        if (PlayerControl.LocalPlayer.Data.Role.TeamType == RoleTeamTypes.Impostor) return;
        if (MeetingHud.Instance != null) return;
        
        if (IsDoingTask)
        {
            TimeSinceLast += Time.deltaTime;
            TotalElapsed += Time.deltaTime;
            
            if (TimeSinceLast > 10f)
            {
                TimeSinceLast = 0f;
                Logger<NarratorModPlugin>.Info($"long task");
                NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.LongTask);
            }
        }
        else
        {
            IdleTime += Time.deltaTime;
            
            if (IdleTime > 20f)
            {
                IdleTime = 0f;
                Logger<NarratorModPlugin>.Info($"no task");
                NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.NoTask);
            }
        }
    }

    [HarmonyPatch(typeof(ProcessDataMinigame), nameof(ProcessDataMinigame.StartStopFill))]
    [HarmonyPostfix]
    public static void ToggleFillPatch()
    {
        
    }
}