namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentManager : ModSystem
{
    public delegate void EntityComponentAddedCallback(Entity entity);

    public delegate void EntityComponentRemovedCallback(Entity entity);

    public delegate void GlobalComponentAddedCallback(Entity entity);

    public delegate void GlobalComponentRemovedCallback(Entity entity);

    private static event EntityComponentAddedCallback OnEntityComponentAdded;
    private static event EntityComponentRemovedCallback OnEntityComponentRemoved;

    private static event GlobalComponentAddedCallback OnGlobalComponentAdded;
    private static event GlobalComponentRemovedCallback OnGlobalComponentRemoved;

    public override void Unload()
    {
        base.Unload();

        OnEntityComponentAdded = null;
        OnEntityComponentRemoved = null;

        OnGlobalComponentAdded = null;
        OnGlobalComponentRemoved = null;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is added to an <see cref="Entity" />.</param>
    public static void AddEventListener_OnEntityComponentAdded(EntityComponentAddedCallback callback)
    {
        OnEntityComponentAdded += callback;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a component is removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to invoke when a component is removed from an <see cref="Entity" />.</param>
    public static void AddEventListener_OnEntityComponentRemoved(EntityComponentRemovedCallback callback)
    {
        OnEntityComponentRemoved += callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added to an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveEventListener_OnEntityComponentAdded(EntityComponentAddedCallback callback)
    {
        OnEntityComponentAdded -= callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a component is added removed from an <see cref="Entity" />.
    /// </summary>
    /// <param name="callback">The callback to remove from the event.</param>
    public static void RemoveEventListener_OnEntityComponentRemoved(EntityComponentRemovedCallback callback)
    {
        OnEntityComponentRemoved -= callback;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a global component is added to a <see cref="Mod" />.
    /// </summary>
    public static void AddEventListener_OnGlobalComponentAdded(GlobalComponentAddedCallback callback)
    {
        OnGlobalComponentAdded += callback;
    }

    /// <summary>
    ///     Registers an event listener that is invoked when a global component is removed from a <see cref="Mod" />.
    /// </summary>
    public static void AddEventListener_OnGlobalComponentRemoved(GlobalComponentRemovedCallback callback)
    {
        OnGlobalComponentRemoved += callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a global component is added to a <see cref="Mod" />.
    /// </summary>
    public static void RemoveEventListener_OnGlobalComponentAdded(GlobalComponentAddedCallback callback)
    {
        OnGlobalComponentAdded -= callback;
    }

    /// <summary>
    ///     Unregisters an event listener that is invoked when a global component is removed from a <see cref="Mod" />.
    /// </summary>
    public static void RemoveEventListener_OnGlobalComponentRemoved(GlobalComponentRemovedCallback callback)
    {
        OnGlobalComponentRemoved -= callback;
    }
}