using HarmonyLib;
using NarratorMod.API;
using NarratorMod.Events.Enums;
using UnityEngine;

namespace NarratorMod.Events.Patches;

[HarmonyPatch]
public class TaskPatches
{
    private static float TaskTime;
    private static float IdleTime;
    private static bool IsDoingTask;
    
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Begin))]
    [HarmonyPostfix]
    public static void OnTaskStart()
    {
        IsDoingTask = true;
        TaskTime = 0f;
    }
    
    [HarmonyPatch(typeof(Minigame), nameof(Minigame.Close), new Type[0])]
    [HarmonyPostfix]
    public static void OnTaskClose()
    {
        if (IsDoingTask && TaskTime < 10f)
        {
            // took too short on task
            NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.FastTask);
        }

        IsDoingTask = false;
        IdleTime = 0f;
        TaskTime = 0f;

        if (Minigame.Instance is ProcessDataMinigame downloadMinigame)
        {
            NarrationManager.Dequeue("");
            NarrationManager.StopPlayingCurrent();
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcCompleteTask))]
    [HarmonyPostfix]
    public static void OnTaskComplete()
    {
        if (PlayerControl.LocalPlayer.AllTasksCompleted())
        {
            // all tasks completed
            NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.CompleteAllTasks);
        }
    }

    public static void Deteriorate()
    {
        if (IsDoingTask)
        {
            TaskTime += Time.deltaTime;
            
            if (TaskTime > 10f)
            {
                TaskTime = 0f;
                NarrationEvents.RaiseCrewmateEvent(CrewmateEvents.LongTask);
            }
        }
        else
        {
            IdleTime += Time.deltaTime;
            
            if (IdleTime > 20f)
            {
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