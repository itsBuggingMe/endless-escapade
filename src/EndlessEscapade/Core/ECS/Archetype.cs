using EndlessEscapade.Core.Collections._Generic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;

namespace EndlessEscapade.Core.ECS;

public partial class Archetype
{
    private static int nextArchetypeID;

    private static FastStack<(ImmutableArray<ComponentID> Components, byte[] IndexMap)> archetypeMetadata = new();
    private static readonly Dictionary<(ulong h1, ulong h2), ArchetypeID> ExistingArchetypeIDs = [];

    public static ArchetypeID GetArchetypeID(Span<Type> types)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(types.Length, byte.MaxValue);

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

        ushort newRawId = checked((ushort)nextArchetypeID);

        return id = ArchetypeID.CreateNew(newRawId);
    }

    public static Archetype Create(ArchetypeID id)
    {
        ref var arr = ref archetypeMetadata[id.GetRawValue()];
        var components = arr.Components;

        ComponentStorage[] storages = new ComponentStorage[components.Length];
        for(int i = 0; i < components.Length; i++)
        {
            storages[i] = Component.Create(components[i]);
        }

        return new Archetype(storages, arr.IndexMap);
    }
}

public partial class Archetype(ComponentStorage[] storages, byte[] indexMap)
{
    internal readonly ComponentStorage[] Storages = storages;
    private readonly byte[] _indexMap = indexMap;
    private readonly ComponentStorage<EntityLight> _entities = new();
    private int _nextIndex;
    public int Create()
    {
        throw new NotImplementedException();
        if(_nextIndex++ == _entities.Capacity)
        {

        }
    }

    /// <summary>
    /// Moves the top entity's components into <paramref name="index"/> and clears the top slot if needed.
    /// </summary>
    /// <returns>The entity id and version of the entity that was moved down</returns>
    public EntityLight Delete(int index)
    {
        foreach(var stor in Storages)
            stor.Delete(index);
        var @return = _entities[_entities.Capacity - 1];
        _entities.Delete(index);
        return @return;
    }

    public ref T GetComponent<T>(int index) => ref ((ComponentStorage<T>)Storages[_indexMap[Component<T>.ID.GetRawValue()]])[index];
    public ref T GetComponentKnownComponentStorageIndex<T>(int index, int storageIndex) => 
        ref ((ComponentStorage<T>)Storages[storageIndex])[index];
    public bool HasComponent<T>(out int index) => (index = _indexMap[Component<T>.ID.GetRawValue()]) != -1;
}