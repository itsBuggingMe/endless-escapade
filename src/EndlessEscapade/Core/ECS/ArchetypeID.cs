using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;

public struct ArchetypeID
{
    private ushort _value;




    internal ushort GetRawValue() => _value;
    internal static ArchetypeID CreateNew(ushort value) => new() { _value = value };
}
