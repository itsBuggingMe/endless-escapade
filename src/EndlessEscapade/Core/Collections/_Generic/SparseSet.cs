using EndlessEscapade.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;

namespace EndlessEscapade.Framework.Collections;

public sealed class SparseSet<T> : IEnumerable<T>
{
    /// <summary>
    ///     Gets the number of elements that the <see cref="SparseSet{T}"/> can hold without resizing.
    /// </summary>
    public int Capacity => _dense.Length;

    /// <summary>
    ///     Gets the number of elements contained in the <see cref="SparseSet{T}"/>.
    /// </summary>
    public int Count => _nextIndex;

    private int _nextIndex;

    private int _version;

    private T[] _dense;

    // this collection should never be empty
    private int[] _sparse;

    private const string INVALID_ID = "ID not in sparse set!";

    public ref T this[int id]
    {
        get
        {
            ref var index = ref EnsureSparseCapacityAndGetIndex(id);

            if (index == -1)
                index = _nextIndex++;

            return ref EnsureDenseCapacityAndGetSlot(index);
        }
    }

    public SparseSet(int capacity = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        _dense = new T[capacity];
        _sparse = new int[capacity];
    }

    public IEnumerator<T> GetEnumerator() => new SparseSetEnumerator(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public ref T Get(int id)
    {
        var localSparse = _sparse;
        if(!((uint)id < (uint)localSparse.Length))
        {//out of range
            ThrowHelper.Throw_ArgumentOutOfRange(INVALID_ID, id);
        }
        var index = localSparse[id];
        
        var localDense = _dense;
        if (!((uint)index < (uint)localDense.Length))
        {
            ThrowHelper.Throw_ArgumentOutOfRange(INVALID_ID, id);
        }

        return ref localDense[index];
    }
    
    public bool TryGet(int id, [MaybeNullWhen(false)] out T value)
    {
        var localSparse = _sparse;
        if (!((uint)id < (uint)localSparse.Length))
            goto doesntExist;

        var index = localSparse[id];

        var localDense = _dense;
        if (!((uint)index < (uint)localDense.Length))
            goto doesntExist;

        value = localDense[index];
        return false;

        //saves a bit of code size
    doesntExist:
        value = default;
        return false;
    }

    public void SetOrAdd(int id, T item) => this[id] = item;

    public bool Remove(int id)
    {
        int moveDownIndex = --_nextIndex;

        var localSparse = _sparse;

        if (!((uint)id < (uint)localSparse.Length))
            return false;

        int moveIntoIndex = localSparse[id];

        var localDense = _dense;
        if (!((uint)moveIntoIndex < (uint)localDense.Length))
            return false;//here, moveIntoIndex should really only ever be -1. We check against len to elide bounds check

        ref T from = ref localDense[moveDownIndex];
        localDense[moveIntoIndex] = from;

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            from = default!;

        return true;
    }

    public bool Has(int id)
    {
        var sparse = _sparse;
        if (!((uint)id < (uint)sparse.Length))
            return false;
        return sparse[id] != -1;
    }
    
    public void EnsureCapacity(int capacity)
    {
        if(_dense.Length < capacity)
        {
            Array.Resize(ref _dense, capacity);
        }
    }

    /// <summary>
    /// Note: this span will become invalid on resize or add
    /// </summary>
    public Span<T> AsSpan() => _dense.AsSpan(0, _nextIndex);

    public void Clear()
    {
        _nextIndex = 0;
        _sparse.AsSpan().Fill(-1);
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            _dense.AsSpan().Clear();
    }

    private ref int EnsureSparseCapacityAndGetIndex(int id)
    {
        var localSparse = _sparse;
        if((uint)id < (uint)localSparse.Length)
        {
            return ref localSparse[id];
        }

        return ref ResizeArrayAndGet(ref _sparse, id);

        static ref int ResizeArrayAndGet(ref int[] arr, int index)
        {
            int prevLen = arr.Length;
            Array.Resize(ref arr, (int)BitOperations.RoundUpToPowerOf2((uint)index + 1));
            arr.AsSpan(prevLen).Fill(-1);
            return ref arr[index];
        }
    }

    private ref T EnsureDenseCapacityAndGetSlot(int index)
    {
        var localDense = _dense;
        if ((uint)index < (uint)localDense.Length)
        {
            return ref localDense[index];
        }

        return ref ResizeArrayAndGet(ref _dense, index);

        static ref T ResizeArrayAndGet(ref T[] arr, int index)
        {
            Array.Resize(ref arr, (int)BitOperations.RoundUpToPowerOf2((uint)index + 1));
            return ref arr[index];
        }
    }

    public struct SparseSetEnumerator(SparseSet<T> set) : IEnumerator<T>
    {
        private readonly SparseSet<T> _toEnumerate = set;
        private readonly int _version = set._version;
        private int _index = -1;

        public readonly ref T Current => ref _toEnumerate._dense[_index];

        public bool MoveNext()
        {
            if (_version != _toEnumerate._version)
                ThrowHelper.Throw_InvalidOperation("Collection has been modified, cannot continue iteration.");
            return ++_index < _toEnumerate._nextIndex;
        }

        public void Reset() => _index = -1;

        readonly object? IEnumerator.Current => Current;
        readonly T IEnumerator<T>.Current => _toEnumerate._dense[_index];
        public readonly void Dispose() { }
    }
}