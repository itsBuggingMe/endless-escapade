using System.Collections.Generic;
using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Core.ECS;

public sealed class EntityRegistry
{
    public int Capacity { get; private set; }

    private readonly Queue<int> indices;
    private readonly SparseSet<int> entities;

    private int nextEntityId;

    public EntityRegistry(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

        Capacity = capacity;

        indices = new Queue<int>(capacity);
        entities = new SparseSet<int>(capacity);
    }

    public Entity Create()
    {
        var id = indices.TryDequeue(out var value) ? value : nextEntityId++;
        var entity = new Entity(id);

        entities.Add(id, id);

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

        return true;
    }

    public bool Has(int id)
    {
        return entities.Has(id);
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
}