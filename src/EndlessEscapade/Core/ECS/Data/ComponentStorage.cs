using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS.Data;

public abstract class ComponentStorage
{
    /// <summary>
    /// Moves the top component into <paramref name="index"/>, clearing the top spot if needed.
    /// </summary>
    public abstract void Delete(int index, int capacity);

    /// <summary>
    /// Returns and boxes the component at <paramref name="index"/>
    /// </summary>
    public abstract object? Box(int index);

    /// <summary>
    /// Pulls a component from another component storage of the same type, and removes it from the other one.
    /// This causes two copy operations and potentially one clear operation.
    /// </summary>
    public abstract void Pull(Archetype other, int otherIndex, int myIndex, int otherTop);

    public abstract void Resize(int size);
}

public sealed class ComponentStorage<T> : ComponentStorage
{
    public int Capacity => _items.Length;

    public override void Resize(int size) => Array.Resize(ref _items, size);

    public override void Pull(Archetype other, int otherIndex, int myIndex, int otherTop)
    {
        var typedComponentStorage = other.GetComponentStorage<T>();

        // key
        // x == item, - == empty

        // this        |   other
        // x           |   x
        // x           |   x <- otherIndex/center
        // x           |   x
        // - <- myIndex|   x <- otherTop
        // -           |   -

        //center -> myIndex
        //otherIndex -> center
        //clear otherIndex

        var localOther = typedComponentStorage._items;
        if (!((uint)otherIndex < (uint)localOther.Length && (uint)otherTop < (uint)localOther.Length))
            ThrowHelper.Throw_ArgumentOutOfRange("Invalid Index", otherIndex);

        ref var center = ref localOther[otherIndex];

        this[myIndex] = center;


        ref var top = ref localOther[otherTop];
        center = top;
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            top = default;
    }

    public override void Delete(int index, int capacity)
    {
        var local = _items;
        if (!((uint)index < (uint)local.Length && (uint)capacity < (uint)local.Length))
            ThrowHelper.Throw_ArgumentOutOfRange("Invalid Index", index);

        ref var top = ref local[capacity];
        ref var bottom = ref local[index];

        bottom = top;
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
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
            if ((uint)index < (uint)locItems.Length)
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

    public override object? Box(int index) => _items[index];
}