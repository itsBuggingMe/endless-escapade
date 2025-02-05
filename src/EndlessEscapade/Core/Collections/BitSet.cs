namespace EndlessEscapade.Framework.Collections;

public sealed class BitSet
{
    private const byte ULONG_SIZE_IN_BITS = sizeof(ulong) * 8;

    /// <summary>
    ///     Gets the number of elements that the <see cref="BitSet"/> can hold without resizing.
    /// </summary>
    public int Capacity { get; private set; }
    
    private ulong[] flags;
    
    public BitSet(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

        Capacity = capacity;

        flags = new ulong[capacity];
    }

    public bool this[int index]
    {
        get => Has(index);
        set => Set(index, value);
    }

    /// <summary>
    ///     Sets the bit at the specified index to the specified value.
    /// </summary>
    /// <param name="index">The index of the bit to set.</param>
    /// <param name="value">The value of the bit to set.</param>
    public void Set(int index, bool value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        
        var arrayIndex = (ulong)(index >> 6);
        var bitOffset = 1UL << (index & ULONG_SIZE_IN_BITS - 1);

        if (value)
        {
            flags[arrayIndex] |= bitOffset;
        }
        else
        {
            flags[arrayIndex] &= ~bitOffset;
        }
    }

    /// <summary>
    ///     Checks whether the bit at the specified index is set.
    /// </summary>
    /// <param name="index">The index of the bit to check.</param>
    /// <returns><c>true</c> if the bit is set; otherwise, <c>false</c>.</returns>
    public bool Has(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        
        var arrayIndex = (ulong)(index >> 6);
        var bitOffset = 1UL << (index & ULONG_SIZE_IN_BITS - 1);

        return (flags[arrayIndex] & bitOffset) != 0;
    }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="size"></param>
    public void Resize(int size)
    {
        Array.Resize(ref flags, size);
        
        Capacity = size;
    }

    public void EnsureCapacity(int capacity)
    {
        if (capacity <= Capacity)
        {
            return;
        }
        
        var newCapacity = Math.Max(1, Capacity);
            
        while (newCapacity <= capacity)
        {
            newCapacity *= 2;
        }

        Resize(newCapacity);
    }

    public void Clear()
    {
        Array.Clear(flags);
    }
}