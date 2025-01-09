using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Collections;

public sealed class SparseSet<T> : IEnumerable<T>, IDisposable
{
    public int Capacity { get; private set; }
    
    public int Count { get; private set; }

    private T[] data;

    private int[] dense;
    private int[] sparse;
    
    public SparseSet(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));
        
        Capacity = capacity;

        data = new T[capacity];
        dense = new int[capacity];
        sparse = new int[capacity];
        
        Array.Fill(sparse, -1); 
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Capacity; i++)
        {
            if (!Has(i))
            {
                continue;
            }
            
            yield return data[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ref T Get(int id)
    {
        if (id < 0 || id >= Count)
        {
            throw new IndexOutOfRangeException($"Index {id} is out of range.");
        }
        
        return ref data[sparse[id]];
    }
    
    public bool TryGet(int id, [MaybeNullWhen(false)] out T entity)
    {
        entity = default;

        if (!Has(id))
        {
            return false;
        }

        entity = Get(id);

        return true;
    }
    
    public bool Add(int id, T value)
    {
        if (Has(id))
        {
            return false;
        }
        
        EnsureCapacity(id + 1);

        data[Count] = value;
        sparse[id] = Count;
        dense[Count] = id;
        
        Count++;

        return true;
    }

    public void Set(int id, T value)
    {
        EnsureCapacity(id + 1);

        data[id] = value;
    }

    public bool Remove(int id)
    {
        if (!Has(id))
        {
            return false;
        }

        var denseIndex = sparse[id];

        if (denseIndex == -1)
        {
            return false;
        }

        var index = sparse[id];

        var lastCount = Count - 1;
        var lastIndex = dense[lastCount];

        if (index != lastCount)
        {
            data[index] = data[lastCount];
            dense[index] = lastIndex;
            sparse[lastIndex] = index;
        }
        
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            data[lastCount] = default;
        }
        
        sparse[id] = -1;
        
        Count--;

        return true;
    }

    public bool Has(int id)
    {
        return id >= 0 && id < Capacity && sparse[id] != -1;
    }
    
    public void Resize(int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size, nameof(size));

        var shrinking = size < Count;
        var growing = size > Count;
        
        if (shrinking)
        {
            for (int i = size; i < Count; i++)
            {
                if (dense[i] < 0)
                {
                    sparse[dense[i]] = -1;
                }
            }

            Count = size;
        }

        Array.Resize(ref data, size);
        Array.Resize(ref dense, size);
        Array.Resize(ref sparse, size);

        if (growing)
        {
            Array.Fill(sparse, -1, Count, size - Count);

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                Array.Fill(dense, default, Count, size - Count);
            }
        }

        Capacity = size;
    }
    
    public void EnsureCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

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
        if (Count <= 0)
        {
            return;
        }
        
        Array.Clear(data);
        Array.Fill(sparse, -1);
        
        Count = 0;
    }

    public void Dispose()
    {
        data = null;
        dense = null;
        sparse = null;
    }
}