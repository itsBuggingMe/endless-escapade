using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentSystem : ModSystem
{
    private static partial class ComponentData<T> where T : struct
    {
        public static T[] Components = [];
    }

    public delegate void ComponentAddedCallback(Entity entity);

    public delegate void ComponentRemovedCallback(Entity entity);

    internal static ulong[] Flags = [];

    private static event ComponentAddedCallback OnComponentAdded;
    private static event ComponentRemovedCallback OnComponentRemoved;

    public override void Unload()
    {
        base.Unload();

        OnComponentAdded = null;
        OnComponentRemoved = null;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is added to an <see cref="Entity" />.</param>
    public static void AddComponentAddedListener(ComponentAddedCallback callback)
    {
        OnComponentAdded += callback;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is removed from an <see cref="Entity" />.</param>
    public static void AddComponentRemovedListener(ComponentRemovedCallback callback)
    {
        OnComponentRemoved += callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveComponentAddedListener(ComponentAddedCallback callback)
    {
        OnComponentAdded -= callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveComponentRemovedListener(ComponentRemovedCallback callback)
    {
        OnComponentRemoved -= callback;
    }

    public static ref T Get<T>(int id) where T : struct
    {
        if (!Has<T>(id))
        {
            throw new ComponentNotFoundException($"Entity {id} does not have a component of type {typeof(T).FullName}");
        }

        return ref ComponentData<T>.Components[id];
    }

    public static void Set<T>(int id, T value) where T : struct
    {
        if (!EntitySystem.TryGet(id, out var entity))
        {
            throw new InvalidEntityException($"Entity {id} does not exist.");
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

    /// <summary>
    ///     Checks whether an <see cref="Entity"/> has a component or not.
    /// </summary>
    /// <param name="id">The identity of the <see cref="Entity"/> to check.</param>
    /// <typeparam name="T">The type of the component to check.</typeparam>
    /// <returns><c>true</c> if the <see cref="Entity"/> has the component; otherwise, <c>false</c>.</returns>
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