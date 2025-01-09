using System.Collections.Generic;
using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Core.ECS;

public sealed class EntityRegistry : IDisposable
{
    public int Capacity { get; private set; }

    private int nextEntityId;
    
    private Queue<int> indices;
    private BitSet flags;
    private SparseSet<int> entities;
    
    public EntityRegistry(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

        Capacity = capacity;

        indices = new Queue<int>(capacity);
        flags = new BitSet(capacity);
        entities = new SparseSet<int>(capacity);
    }

    public Entity Create()
    {
        var id = indices.TryDequeue(out var value) ? value : nextEntityId++;
        var entity = new Entity(id);

        entities.Add(id, id);
        
        flags.Set(id, true);

        return entity;
    }

    public bool Destroy(int id)
    {
        if (!Has(id))
        {
            return false;
        }
        
        indices.Enqueue(id);
        entities.Remove(id);

        flags.Set(id, false);

        return true;
    }

    public bool Has(int id)
    {
        return flags.Has(id);
    }

    public Entity Get(int id)
    {
        if (!Has(id))
        {
            throw new InvalidEntityException($"Entity {id} does not exist.");
        }

        return new Entity(id);
    }

    public bool TryGet(int id, out Entity entity)
    {
        entity = default;

        if (!Has(id))
        {
            return false;
        }

        entity = new Entity(id);

        return true;
    }
    
    public void Dispose()
    {
        indices = null!;
        flags = null!;
        entities = null!;
    }
}