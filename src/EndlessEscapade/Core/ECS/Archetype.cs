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

    private static FastStack<ImmutableArray<ComponentID>> ArchetypeMetadata = new();
    private static readonly Dictionary<(ulong h1, ulong h2), ArchetypeID> existingArchetypeIDs = [];

    public static ArchetypeID GetArchetypeID<T>()
        where T : struct, IRec
    {
        default(T).AppendTypes(IRec.SharedTypeList);

        ulong hash1 = 0;
        ulong hash2 = 0;
        foreach (var type in IRec.SharedTypeList)
        {
            hash1 ^= (ulong)type.GetHashCode() * 98317U;
            hash2 += (ulong)type.GetHashCode() * 53U;
        }
        IRec.SharedTypeList.Clear();

        ref ArchetypeID id = ref CollectionsMarshal.GetValueRefOrAddDefault(existingArchetypeIDs, (hash1, hash2), out bool exists);
        if (exists)
            return id;

        ushort newRawId = checked((ushort)nextArchetypeID);

        return id = ArchetypeID.CreateNew(newRawId);
    }

    public static Archetype Create(ArchetypeID id)
    {
        var arr = ArchetypeMetadata[id.GetRawValue()];
        ComponentStorage[] storages = new ComponentStorage[arr.Length];
        for(int i = 0; i < arr.Length; i++)
        {
            storages[i] = Component.Create(arr[i]);
        }

        //TODO: index map
        throw new NotImplementedException();
        return new Archetype(storages, null!);
    }
}

public partial class Archetype(ComponentStorage[] storages, byte[] indexMap)
{
    private readonly ComponentStorage[] _storages = storages;
    private readonly byte[] _indexMap = indexMap;


    public int Create()
    {
        throw new NotImplementedException();
    }

    public ref T GetComponent<T>(int index)
    {
        throw new NotImplementedException();
    }
}