using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentSystem : ModSystem
{
    private static partial class ComponentData<T> where T : struct
    {
        public static T[] Components = [];
    }

    internal static ulong[] Flags = [];

    public static event Action<Entity> OnComponentAdded;
    public static event Action<Entity> OnComponentRemoved;

    public override void Unload()
    {
        base.Unload();

        OnComponentAdded = null;
        OnComponentRemoved = null;
    }

    public static ref T Get<T>(int id) where T : struct
    {
        if (!Has<T>(id))
        {
            throw new InvalidOperationException();
        }
        
        return ref ComponentData<T>.Components[id];
    }

    public static void Set<T>(int id, T value) where T : struct
    {
        if (!EntitySystem.TryGet(id, out var entity))
        {
            return;
        }
        
        ArrayUtils.EnsureCapacity(ref ComponentData<T>.Components, id);

        var componentId = ComponentData<T>.Id;

        var masks = MathUtils.DivCeil(ComponentTypeCount, MaskSize);
        var index = id * masks + Math.DivRem(componentId, MaskSize, out var remainder);

        ArrayUtils.EnsureCapacity(ref Flags, index);

        var mask = 1UL << remainder;

        Flags[index] |= mask;

        ComponentData<T>.Components[id] = value;

        OnComponentAdded?.Invoke(entity);
    }
    
    public static bool Has<T>(int id) where T : struct
    {
        if (id < 0 || id >= ComponentData<T>.Components.Length || !EntitySystem.TryGet(id, out _))
        {
            return false;
        }

        var componentId = ComponentData<T>.Id;

        var masks = MathUtils.DivCeil(ComponentTypeCount, MaskSize);
        var index = id * masks + Math.DivRem(componentId, MaskSize, out var remainder);

        if (index < 0 || index >= Flags.Length)
        {
            return false;
        }

        var mask = 1UL << remainder;

        return (Flags[index] & mask) != 0;
    }

    public static bool Remove<T>(int id) where T : struct
    {
        if (id < 0 || id >= ComponentData<T>.Components.Length || !EntitySystem.TryGet(id, out var entity))
        {
            return false;
        }

        var componentId = ComponentData<T>.Id;

        var masks = MathUtils.DivCeil(ComponentTypeCount, MaskSize);
        var index = id * masks + Math.DivRem(componentId, MaskSize, out var remainder);

        if (index < 0 || index >= Flags.Length)
        {
            return false;
        }

        var mask = 1UL << remainder;

        Flags[index] &= ~mask;

        OnComponentRemoved?.Invoke(entity);

        return true;
    }
}