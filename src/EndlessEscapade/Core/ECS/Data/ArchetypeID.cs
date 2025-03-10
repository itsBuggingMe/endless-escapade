using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS.Data;

public struct ArchetypeID
{
    private ushort _value;

    public readonly ImmutableArray<ComponentID> ComponentIDs => Archetype.MetadataTable[_value].Components;


    internal ushort GetRawValue() => _value;
    internal static ArchetypeID CreateNew(ushort value) => new() { _value = value };
}
