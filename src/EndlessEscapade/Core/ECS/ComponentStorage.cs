using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;

public abstract class ComponentStorage
{
    /// <summary>
    /// Moves the top component into <paramref name="index"/>, clearing the top spot if needed.
    /// </summary>
    public abstract void Delete(int index);

    /// <summary>
    /// Returns and boxes the component at <paramref name="index"/>
    /// </summary>
    public abstract object? Box(int index);

    /// <summary>
    /// Pulls a component from another component storage of the same type, and removes it from the other one.
    /// This causes two copy operations and potentially one clear operation.
    /// </summary>
    public abstract void Pull(ComponentStorage other, int index, int capacity);
}

public sealed class ComponentStorage<T> : ComponentStorage
{
    public int Capacity => _items.Length;

    public override void Pull(ComponentStorage other, int otherIndex, int myCapacity, )
    {
        var typedComponentStorage = (ComponentStorage<T>)other;
        this[capacity] = typedComponentStorage[index];
        typedComponentStorage.Delete(index);
    }

    public override void Delete(int index, int capacity)
    {
        var local = _items;
        if(!(uint)index < (uint)local.Length && (uint)capacity < (uint)local.Length)
            ThrowHelper.Throw_ArgumentOutOfRange("Invalid Index", index);
        
        ref var top = ref local[capacity];
        ref var bottom = ref local[index];

        bottom = top;
        if(RuntimeHelpers.IsReferenceOrConatinsReferences<T>())
            top = default;
    }

    private T[] _items;

    public ComponentStorage()
    {
        _items = [];
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