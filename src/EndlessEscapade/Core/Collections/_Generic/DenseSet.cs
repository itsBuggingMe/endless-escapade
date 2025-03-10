using System.Numerics;

namespace EndlessEscapade.Framework.Collections;

public sealed class DenseSet<T>
{
    private T[] _items;

    public DenseSet(int capacity = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        _items = new T[capacity];
    }

    public ref T this[int index]
    {
        get
        {
            var locItems = _items;
            if ((uint)index < (uint)_items.Length)
            {
                return ref locItems[index];
            }

            return ref ResizeAndGet(index);
        }
    }

    private ref T ResizeAndGet(int index)
    {
        Array.Resize(ref _items, (int)BitOperations.RoundUpToPowerOf2((uint)index + 1));
        return ref _items[index];
    }
}