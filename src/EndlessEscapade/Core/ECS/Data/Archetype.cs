using EndlessEscapade.Core.Collections;
using EndlessEscapade.Core.ECS.Data;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;

namespace EndlessEscapade.Core.ECS;

public partial class Archetype
{
    private static int nextArchetypeID;

    static Archetype()
    {
        _ = GetArchetypeID([]);
    }

    internal static FastStack<(ImmutableArray<ComponentID> Components, byte[] IndexMap)> MetadataTable = new();
    private static readonly Dictionary<(ulong h1, ulong h2), ArchetypeID> ExistingArchetypeIDs = [];

    public static ArchetypeID GetArchetypeID(ReadOnlySpan<ComponentID> types)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(types.Length, byte.MaxValue);

        ulong hash1 = 0;
        ulong hash2 = 0;
        foreach (var type in types)
        {
            hash1 ^= (ulong)type.GetHashCode() * 98317U;
            hash2 += (ulong)type.GetHashCode() * 53U;
        }

        ref ArchetypeID id = ref CollectionsMarshal.GetValueRefOrAddDefault(ExistingArchetypeIDs, (hash1, hash2), out bool exists);
        if (exists)
            return id;

        ushort newRawId = checked((ushort)nextArchetypeID++);

        int max = int.MinValue;
        foreach(var type in types)
            max = Math.Max(max, type.GetRawValue());

        byte[] indexMap = new byte[max];
        indexMap.AsSpan().Fill(byte.MaxValue);
        
        for(int i = 0; i < types.Length; i++)
        {
            indexMap[types[i].GetRawValue()] = (byte)i;
        }

        MetadataTable.Push((types.ToImmutableArray(), indexMap));

        return id = ArchetypeID.CreateNew(newRawId);
    }

    public static Archetype Create(ArchetypeID id)
    {
        ref var arr = ref MetadataTable[id.GetRawValue()];
        var components = arr.Components;

        ComponentStorage[] storages = new ComponentStorage[components.Length];
        for(int i = 0; i < components.Length; i++)
        {
            storages[i] = Component.Create(components[i]);
        }

        return new Archetype(id, storages, arr.IndexMap);
    }
}

public partial class Archetype(ArchetypeID id, ComponentStorage[] storages, byte[] indexMap)
{
    internal readonly ArchetypeID ID = id;
    internal readonly ComponentStorage[] Storages = storages;
    internal readonly byte[] IndexMap = indexMap;
    private readonly ComponentStorage<EntityLight> _entities = new();
    private int _nextIndex;
    public int Create(out Ref<EntityLight> slot)
    {
        if (_nextIndex == _entities.Capacity)
            Resize(_nextIndex << 1);
        slot = new(ref _entities[_nextIndex]);
        return _nextIndex++;
    }

    private void Resize(int newSize)
    {
        foreach (var i in Storages)
            i.Resize(newSize);
        _entities.Resize(newSize);
    }

    /// <summary>
    /// Moves the top entity's components into <paramref name="index"/> and clears the top slot if needed.
    /// </summary>
    /// <returns>The entity id and version of the entity that was moved down</returns>
    public EntityLight Delete(int index)
    {
        int capacity = _entities.Capacity;
        foreach(var stor in Storages)
            stor.Delete(index, capacity);
        var @return = _entities[capacity - 1];
        _entities.Delete(index, capacity);
        return @return;
    }

    public EntityLight DeleteFromEntityStorageOnly(int index)
    {
        int capacity = _entities.Capacity;
        var @return = _entities[capacity - 1];
        _entities.Delete(index, capacity);
        return @return;
    }

    public ref T GetComponent<T>(int index) => ref ((ComponentStorage<T>)Storages[IndexMap[Component<T>.ID.GetRawValue()]])[index];
    public ref T GetComponentKnownComponentStorageIndex<T>(int index, int storageIndex) => 
        ref ((ComponentStorage<T>)Storages[storageIndex])[index];
    public bool HasComponent<T>(out int index) => (index = IndexMap[Component<T>.ID.GetRawValue()]) != -1;
    public ComponentStorage<T> GetComponentStorage<T>() => (ComponentStorage<T>)Storages[IndexMap[Component<T>.ID.GetRawValue()]];
    public int Capacity => _entities.Capacity;
    public int Count => _nextIndex;
}