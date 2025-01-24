<<<<<<<< HEAD:src/EndlessEscapade/Framework/_ECS/_Entities/EntityManager.cs
namespace EndlessEscapade.Core.ECS;
========
﻿namespace EndlessEscapade.Framework;
>>>>>>>> dev:src/EndlessEscapade/Framework/_ECS/_Entities/EntitySystem.cs

public sealed class EntityManager : ILoadable
{
    public delegate void EntityCreatedCallback(Entity entity);

    public delegate void EntityDestroyedCallback(Entity entity);

    public const int INITIAL_ENTITY_COUNT = 1024;

    private static EntityRegistry registry = new(INITIAL_ENTITY_COUNT);

    private static event EntityCreatedCallback OnEntityCreated;
    private static event EntityDestroyedCallback OnEntityDestroyed;
    
    void ILoadable.Load(Mod mod) { }

    void ILoadable.Unload()
    {
        registry?.Dispose();
        registry = null;
    }

    public static Entity Create()
    {
        var entity = registry.Create();

        OnEntityCreated?.Invoke(entity);

        return entity;
    }

    public static bool Destroy(int id)
    {
        if (!registry.Destroy(id))
        {
            return false;
        }

        OnEntityDestroyed?.Invoke(registry.Get(id));

        return true;
    }

    public static bool Has(int id)
    {
        return registry.Has(id);
    }

    public static Entity Get(int id)
    {
        return registry.Get(id);
    }

    public static bool TryGet(int id, out Entity entity)
    {
        return registry.TryGet(id, out entity);
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