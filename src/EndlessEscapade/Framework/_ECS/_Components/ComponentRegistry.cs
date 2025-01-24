using EndlessEscapade.Framework.Collections;

namespace EndlessEscapade.Framework;

public sealed class ComponentRegistry<T> : IDisposable
{
    public int Capacity { get; private set; }

    private BitSet flags;
    private SparseSet<T> data;

    public ComponentRegistry(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));
        
        Capacity = capacity;

        flags = new BitSet(capacity);
        data = new SparseSet<T>(capacity);
    }

    public ref T Get(int id)
    {
        return ref data.Get(id);
    }

    public void Set(int id, T value)
    {
        data.Set(id, value);
    }

    public bool Has(int id)
    {
        return flags.Has(id);
    }

    public bool Remove(int id)
    {
        return data.Remove(id);
    }
    
    public void Dispose()
    {
        data = null!;
        flags = null!;
    }
}