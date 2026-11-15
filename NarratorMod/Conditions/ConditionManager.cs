using System.Reflection;
using Reactor.Utilities;

namespace NarratorMod.Conditions;

public class ConditionManager
{
    public static List<INarrationCondition> RegisteredConditions = new();

    public static void SubscribeAll()
    {
        foreach (var condition in RegisteredConditions)
        {
            condition.Subscribe();
        }
    }
    
    public static void UnsubscribeAll()
    {
        foreach (var condition in RegisteredConditions)
        {
            condition.Unsubscribe();
        }
    }
    
    public static void RegisterCondition(INarrationCondition condition)
    {
        if (RegisteredConditions.Contains(condition)) return;
        
        condition.Subscribe();
        RegisteredConditions.Add(condition);
        
        Logger<NarratorModPlugin>.Info($"Registered condition : {condition.GetType().Name}");
    }

    /*public static void UnregisterCondition(INarrationCondition condition)
    {
        foreach (var registered in RegisteredConditions)
        {
            if (registered.GetType() == condition.GetType())
            {
                RegisteredConditions.Remove(registered);
            }
        }
    }*/
    
    public static void RegisterAllFromAssembly(Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        Type baseType = typeof(INarrationCondition);

        foreach (var type in assembly.GetTypes())
        {
            if (baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract)
            {
                RegisterCondition((INarrationCondition)Activator.CreateInstance(type));
            }
        }
    }
}