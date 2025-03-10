using System.Collections.Generic;
using System.Collections;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Collections._Generic;

/// <summary>
/// This struct is meant to be used purely inside of classes as fields.
/// </summary>
/// <remarks>It is a light wrapper over an array. It does not track versions.</remarks>
public struct FastStack<T> : IEnumerable<T>, IEnumerable
    where T : notnull
{
    private T[] _buffer;
    private int _nextIndex;

    public FastStack() : this(4)
    {

    }

    public FastStack(int initalCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initalCapacity);
        _buffer = initalCapacity == 0 ? [] : new T[initalCapacity];
        _nextIndex = 0;
    }

    public int Count => _nextIndex;

    public ref T this[int index]
    {
        get
        {
            if ((uint)index < (uint)_nextIndex)
            {
                return ref _buffer[index];
            }

            return ref Throw_OutOfRange();
        }
    }

    public void Push(T item)
    {
        var buffer = _buffer;
        if (_nextIndex < _buffer.Length)
        {
            buffer[_nextIndex++] = item;
        }

        ResizeAndPush(item);
    }

    public T Pop()
    {
        var buffer = _buffer;
        if ((uint)(--_nextIndex) < buffer.Length)
        {
            ref T item = ref buffer[_nextIndex];
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                var returnValue = item;
                item = default!;
                return returnValue;
            }
            return item;
        }

        return Throw_EmptyStack();
    }

    public bool TryPop([NotNullWhen(true)] out T? item)
    {
        var buffer = _buffer;
        if ((uint)(--_nextIndex) < buffer.Length)
        {
            ref T slot = ref buffer[_nextIndex];
            item = slot;
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                item = default!;
            return true;
        }

        item = default;
        return false;
    }

    public Span<T> AsSpan() => _buffer.AsSpan(0, _nextIndex);

    private void ResizeAndPush(in T item)
    {
        Array.Resize(ref _buffer, _buffer.Length + (_buffer.Length >> 1));
        _buffer[_nextIndex++] = item;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private T Throw_EmptyStack()
    {
        throw new InvalidOperationException("Stack is empty!");
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    private ref T Throw_OutOfRange()
    {
        throw new ArgumentOutOfRangeException();
    }

    #region Enumerable
    public Enumerator GetEnumerator() => new(this);
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator(FastStack<T> stack) : IEnumerator<T>, IEnumerator
    {
        private T[] _buffer = stack._buffer;
        private int _nextIndex = stack._nextIndex;
        private int _currentIndex = -1;
        public readonly T Current => _buffer[_currentIndex];
        readonly object IEnumerator.Current => Current;

        public readonly void Dispose() { }
        public bool MoveNext() => ++_currentIndex < _nextIndex;
        public void Reset() => _currentIndex = -1;
    }
    #endregion Enumerable
}