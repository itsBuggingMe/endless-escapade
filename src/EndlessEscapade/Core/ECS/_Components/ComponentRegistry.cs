using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Core.ECS;

public sealed class ComponentRegistry<T>(int capacity)
{
    private readonly SparseSet<T> data = new(capacity);
    private readonly BitmaskSet flags = new(capacity);

    public ref T Get(int entityId)
    {
        return ref data.Get(entityId);
    }

    public void Set(int entityId, T value)
    {
        data.Set(entityId, value);
    }

    public bool Has(int entityId)
    {
        return data.Has(entityId);
    }

    public bool Remove(int entityId)
    {
        return data.Remove(entityId);
    }
}