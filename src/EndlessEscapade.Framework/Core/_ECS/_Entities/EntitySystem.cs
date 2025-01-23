namespace EndlessEscapade.Framework.Core;

public static class EntitySystem
{
    public delegate void EntityCreatedCallback(Entity entity);

    public delegate void EntityDestroyedCallback(Entity entity);

    public const int INITIAL_ENTITY_COUNT = 1024;

    private static readonly EntityRegistry Registry = new(INITIAL_ENTITY_COUNT);

    private static event EntityCreatedCallback? OnEntityCreated;
    private static event EntityDestroyedCallback? OnEntityDestroyed;

    public static Entity Create()
    {
        var entity = Registry.Create();

        OnEntityCreated?.Invoke(entity);

        return entity;
    }

    public static bool Destroy(int id)
    {
        if (!Registry.Destroy(id))
        {
            return false;
        }

        OnEntityDestroyed?.Invoke(Registry.Get(id));

        return true;
    }

    public static bool Has(int id)
    {
        return Registry.Has(id);
    }

    public static Entity Get(int id)
    {
        return Registry.Get(id);
    }

    public static bool TryGet(int id, out Entity entity)
    {
        return Registry.TryGet(id, out entity);
    }

    public static void AddEventListener_EntityCreated(EntityCreatedCallback callback)
    {
        OnEntityCreated += callback;
    }

    public static void AddEventListener_EntityDestroyed(EntityDestroyedCallback callback)
    {
        OnEntityDestroyed += callback;
    }

    public static void RemoveEventListener_EntityCreated(EntityCreatedCallback callback)
    {
        OnEntityCreated -= callback;
    }

    public static void RemoveEventListener_EntityDestroyed(EntityDestroyedCallback callback)
    {
        OnEntityDestroyed -= callback;
    }
}