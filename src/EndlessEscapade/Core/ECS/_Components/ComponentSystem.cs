using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.ECS;

public sealed class ComponentSystem : ModSystem
{
    private static class ComponentData<T> where T : struct
    {
        public static readonly int Id = ComponentTypeCount++;

        public static T[] Components = [];
    }

    internal const byte MaskSize = sizeof(ulong) * 8;

    internal static ulong[] Flags = [];

    /// <summary>
    ///     The total amount of component types registered.
    /// </summary>
    public static int ComponentTypeCount { get; private set; }

    /// <summary>
    ///     Invoked every time a component is added to an <see cref="Entity"/>.
    /// </summary>
    public static event Action<Entity> OnComponentAdded;

    /// <summary>
    ///     Invoked every time a component is removed from an <see cref="Entity"/>.
    /// </summary>
    public static event Action<Entity> OnComponentRemoved;

    public override void Unload()
    {
        base.Unload();

        OnComponentAdded = null;
        OnComponentRemoved = null;
    }

    /// <summary>
    ///     Gets the value of a component of the specified type from an <see cref="Entity"/>.
    /// </summary>
    /// <param name="id">The identity of the <see cref="Entity"/> to retrieve the component from.</param>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns></returns>
    public static ref T Get<T>(int id) where T : struct
    {
        return ref ComponentData<T>.Components[id];
    }

    /// <summary>
    ///     Sets the value of a component of the specified type to an <see cref="Entity"/>.
    /// </summary>
    /// <param name="id">The identity of the <see cref="Entity"/> to set the component to.</param>
    /// <param name="value">The value of the component to set.</param>
    /// <typeparam name="T">The type of the component to set.</typeparam>
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

    /// <summary>
    ///     Checks whether an <see cref="Entity"/> has a component of the specified type or not.
    /// </summary>
    /// <param name="id">The identity of the <see cref="Entity"/> to check.</param>
    /// <typeparam name="T">The type of the component to check.</typeparam>
    /// <returns><c>true</c> if the <see cref="Entity"/> has the specified component type; otherwise, <c>false</c>.</returns>
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

    /// <summary>
    ///     Attempts to remove a component of the specified type from an <see cref="Entity"/>.
    /// </summary>
    /// <param name="id">The identity of the <see cref="Entity"/> to remove the component from.</param>
    /// <typeparam name="T">The type of the component to remove.</typeparam>
    /// <returns><c>true</c> if the component was successfully removed from the <see cref="Entity"/>; otherwise, <c>false</c>.</returns>
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