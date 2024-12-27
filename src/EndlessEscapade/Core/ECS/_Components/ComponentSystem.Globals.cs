namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentSystem : ModSystem
{
    private static partial class ComponentData<T> where T : struct
    {
        public static T GlobalComponent;

        public static bool HasGlobalComponent;
    }
    
    public static ref T Get<T>() where T : struct
    {
        if (!Has<T>())
        {
            throw new InvalidOperationException();
        }
        
        return ref ComponentData<T>.GlobalComponent;
    }

    public static void Set<T>(T value) where T : struct
    {
        ComponentData<T>.GlobalComponent = value;
        ComponentData<T>.HasGlobalComponent = true;
    }
    
    public static bool Has<T>() where T : struct
    {
        return ComponentData<T>.HasGlobalComponent;
    }

    public static void Remove<T>() where T : struct
    {
        ComponentData<T>.GlobalComponent = default;
        ComponentData<T>.HasGlobalComponent = false;
    }
}