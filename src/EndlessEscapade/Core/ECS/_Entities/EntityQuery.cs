using System.Collections;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace EndlessEscapade.Core.ECS;

public sealed class EntityQuery<T> : IEnumerable<Entity> where T : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.OnComponentAdded += OnComponentAdded;
        ComponentSystem.OnComponentRemoved += OnComponentRemoved;
    }

    public IEnumerator<Entity> GetEnumerator()
    {
        foreach (var entity in Entities)
        {
            yield return entity;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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

public sealed class EntityQuery<T1, T2> : IEnumerable<Entity>
    where T1 : struct
    where T2 : struct
{
    private static readonly List<Entity> Entities = new();

    static EntityQuery()
    {
        ComponentSystem.OnComponentAdded += OnComponentAdded;
        ComponentSystem.OnComponentRemoved += OnComponentRemoved;
    }

    public IEnumerator<Entity> GetEnumerator()
    {
        foreach (var entity in Entities)
        {
            yield return entity;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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