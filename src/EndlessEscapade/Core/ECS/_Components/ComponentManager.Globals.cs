namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentManager : ModSystem
{
    private static class ComponentGlobalData<T> where T : struct
    {
        public static readonly GlobalComponentRegistry<T> Registry = new();
    }
    
    public static ref T Get<T>() where T : struct
    {
        if (!ComponentGlobalData<T>.Registry.Has())
        {
            throw new ComponentNotFoundException($"Mod does not have a global component of type {typeof(T).FullName}");
        }
        
        return ref ComponentGlobalData<T>.Registry.Get();
    }

    public static void Set<T>(T value) where T : struct
    {
        ComponentGlobalData<T>.Registry.Set(value);
    }

    public static bool Remove<T>() where T : struct
    {
        return ComponentGlobalData<T>.Registry.Has();
    }

    public static bool Has<T>() where T : struct
    {
        return ComponentGlobalData<T>.Registry.Has();
    }
}