namespace EndlessEscapade.Framework;

public sealed class ComponentSystem : ModSystem
{
    private static class ComponentData<T> where T : struct
    {
        public static readonly ComponentRegistry<T> Registry = new(EntitySystem.INITIAL_ENTITY_COUNT);
    }

    public delegate void ComponentAddedCallback(Entity entity);

    public delegate void ComponentRemovedCallback(Entity entity);

    private static event ComponentAddedCallback? OnComponentAdded;
    private static event ComponentRemovedCallback? OnComponentRemoved;

    public override void Unload()
    {
        base.Unload();

        OnComponentAdded = null;
        OnComponentRemoved = null;
    }

    public static ref T Get<T>(int entityId) where T : struct
    {
        if (!ComponentData<T>.Registry.Has(entityId))
        {
            throw new ComponentNotFoundException($"Entity {entityId} does not have a component of type {typeof(T).FullName}");
        }

        return ref ComponentData<T>.Registry.Get(entityId);
    }

    public static void Set<T>(int entityId, T value) where T : struct
    {
        if (!EntitySystem.Has(entityId))
        {
            throw new InvalidEntityException($"Entity {entityId} does not exist.");
        }

        ComponentData<T>.Registry.Set(entityId, value);

        OnComponentAdded?.Invoke(EntitySystem.Get(entityId));
    }

    public static bool Remove<T>(int entityId) where T : struct
    {
        if (!ComponentData<T>.Registry.Remove(entityId))
        {
            return false;
        }

        OnComponentRemoved?.Invoke(EntitySystem.Get(entityId));

        return true;
    }

    public static bool Has<T>(int entityId) where T : struct
    {
        return ComponentData<T>.Registry.Has(entityId);
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is added to an <see cref="Entity" />.</param>
    public static void AddEventListener_OnEntityComponentAdded(ComponentAddedCallback callback)
    {
        OnComponentAdded += callback;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is removed from an <see cref="Entity" />.</param>
    public static void AddEventListener_OnEntityComponentRemoved(ComponentRemovedCallback callback)
    {
        OnComponentRemoved += callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveEventListener_OnEntityComponentAdded(ComponentAddedCallback callback)
    {
        OnComponentAdded -= callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveEventListener_OnEntityComponentRemoved(ComponentRemovedCallback callback)
    {
        OnComponentRemoved -= callback;
    }
}