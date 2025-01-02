using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Collections;

// TODO: Impl
public sealed class BitmaskSet
{
    public int Capacity { get; private set; }
    
    private ulong[] flags;
    
    public BitmaskSet(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

        Capacity = capacity;
    }
}