using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Core.ECS;

public sealed class ComponentRegistry<T> : IDisposable
{
    public int Capacity { get; private set; }

    private BitmaskSet flags;
    private SparseSet<T> data;

    public ComponentRegistry(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));
        
        Capacity = capacity;

        flags = new BitmaskSet(capacity);
        data = new SparseSet<T>(capacity);
    }

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
        return flags.Has(entityId);
    }

    public bool Remove(int entityId)
    {
        return data.Remove(entityId);
    }
    
    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            data?.Dispose();
        }

        data = null;
        flags = null;
    }
}