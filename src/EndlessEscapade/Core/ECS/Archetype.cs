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
    private readonly ComponentStorage[] _storages = storages;
    private readonly byte[] _indexMap = indexMap;
    private int _nextIndex;
    private int _capacity;

    public int Create()
    {
        throw new NotImplementedException();
        if(_nextIndex++ == _capacity)
        {

        }
    }

    public ref T GetComponent<T>(int index) => ref ((ComponentStorage<T>)_storages[_indexMap[index]])[index];
}