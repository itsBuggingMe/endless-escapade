using System.Collections.Generic;

namespace EndlessEscapade.Framework;

public static class EntityQuery<T>
    where T : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.AddEventListener_OnEntityComponentAdded(OnComponentAdded);
        ComponentSystem.AddEventListener_OnEntityComponentRemoved(OnComponentRemoved);
    }

    public static IEnumerable<Entity> Enumerate()
    {
        return Entities;
    }

    private static void OnComponentAdded(Entity entity)
    {
        if (!entity.Has<T>())
        {
            return;
        }

        Entities.Add(entity);
    }

    private static void OnComponentRemoved(Entity entity)
    {
        if (!entity.Has<T>())
        {
            return;
        }

        Entities.Remove(entity);
    }
}

public static class EntityQuery<T1, T2>
    where T1 : struct
    where T2 : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.AddEventListener_OnEntityComponentAdded(OnComponentAdded);
        ComponentSystem.AddEventListener_OnEntityComponentRemoved(OnComponentRemoved);
    }

    public static IEnumerable<Entity> Enumerate()
    {
        return Entities;
    }

    private static void OnComponentAdded(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>())
        {
            return;
        }

        Entities.Add(entity);
    }

    private static void OnComponentRemoved(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>())
        {
            return;
        }

        Entities.Remove(entity);
    }
}

public static class EntityQuery<T1, T2, T3>
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.AddEventListener_OnEntityComponentAdded(OnComponentAdded);
        ComponentSystem.AddEventListener_OnEntityComponentRemoved(OnComponentRemoved);
    }

    public static IEnumerable<Entity> Enumerate()
    {
        return Entities;
    }

    private static void OnComponentAdded(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>())
        {
            return;
        }

        Entities.Add(entity);
    }

    private static void OnComponentRemoved(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>())
        {
            return;
        }

        Entities.Remove(entity);
    }
}

public static class EntityQuery<T1, T2, T3, T4>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.AddEventListener_OnEntityComponentAdded(OnComponentAdded);
        ComponentSystem.AddEventListener_OnEntityComponentRemoved(OnComponentRemoved);
    }

    public static IEnumerable<Entity> Enumerate()
    {
        return Entities;
    }

    private static void OnComponentAdded(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>() || !entity.Has<T4>())
        {
            return;
        }

        Entities.Add(entity);
    }

    private static void OnComponentRemoved(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>() || !entity.Has<T4>())
        {
            return;
        }

        Entities.Remove(entity);
    }
}

public static class EntityQuery<T1, T2, T3, T4, T5>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.AddEventListener_OnEntityComponentAdded(OnComponentAdded);
        ComponentSystem.AddEventListener_OnEntityComponentRemoved(OnComponentRemoved);
    }

    public static IEnumerable<Entity> Enumerate()
    {
        return Entities;
    }

    private static void OnComponentAdded(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>() || !entity.Has<T4>() || !entity.Has<T5>())
        {
            return;
        }

        Entities.Add(entity);
    }

    private static void OnComponentRemoved(Entity entity)
    {
        if (!entity.Has<T1>() || !entity.Has<T2>() || !entity.Has<T3>() || !entity.Has<T4>() || !entity.Has<T5>())
        {
            return;
        }

        Entities.Remove(entity);
    }
}