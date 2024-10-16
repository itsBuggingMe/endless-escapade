namespace EndlessEscapade.Core.EC;

public sealed partial class ComponentSystem : ModSystem
{
    private static partial class ComponentData<T> where T : struct
    {
        public static T Global;

        public static bool HasGlobal;
    }

    public static ref T Get<T>() where T : struct {
        return ref ComponentData<T>.Global;
    }

    public static ref T Set<T>(T value) where T : struct {
        ComponentData<T>.HasGlobal = true;

        return ref ComponentData<T>.Global;
    }

    public static bool Has<T>() where T : struct {
        return ComponentData<T>.HasGlobal;
    }

    public static bool Remove<T>() where T : struct {
        ComponentData<T>.HasGlobal = false;

        return true;
    }
}
