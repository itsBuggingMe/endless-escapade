using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS.Data;

public struct ComponentID : IEquatable<ComponentID>
{
    private ushort _value;
    public Type Type => Component.ComponentMetaDataTable[_value].UnderlyingType;
    public bool Equals(ComponentID other) => other._value == _value;
    public static bool operator ==(ComponentID left, ComponentID right) => left.Equals(right);
    public static bool operator !=(ComponentID left, ComponentID right) => !left.Equals(right);
    public ushort GetRawValue() => _value;
    public static ComponentID CreateFromRawValue(ushort raw) => new() { _value = raw };
}
