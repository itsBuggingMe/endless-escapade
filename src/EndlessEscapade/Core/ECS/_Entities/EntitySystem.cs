using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EndlessEscapade.Core.Collections;
using EndlessEscapade.Utilities;
using Terraria.DataStructures;

namespace EndlessEscapade.Core.ECS;

public sealed class EntitySystem : ModSystem
{
    public const int INITIAL_ENTITY_COUNT = 1024;
    
    private static readonly Queue<int> Indices = [];
    private static readonly SparseSet<int> Entities = new(INITIAL_ENTITY_COUNT);

    private static int nextEntityId;

    public static Entity Create()
    {
        int id;

        if (!Indices.TryDequeue(out id))
        {
            id = nextEntityId++;
        }

        var entity = new Entity(id);

        Entities.Add(id, id);
        
        return entity;
    }

    public static bool Destroy(int id)
    {
        if (!Has(id))
        {
            return false;
        }
        
        Entities.Remove(id);

        return true;
    }

    public static bool Has(int id)
    {
        return Entities.Has(id);
    }

    public static SparseSet<int> Enumerate() => Entities;

    public static Entity Get(int id)
    {
        if (!TryGet(id, out var entity))
        {
            throw new InvalidEntityException($"Entity {id} does not exist.");
        }

        return entity;
    }

    public static bool TryGet(int id, out Entity entity)
    {
        entity = default;
        
        if (!Entities.TryGet(id, out var index))
        {
            return false;
        }

        entity = new Entity(index);

        return true;
    }
}