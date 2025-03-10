using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;
internal class Component<T>
{
    public static readonly ComponentID ID = default;
    public static ComponentStorage Create() => throw new NotImplementedException();
}

internal class Component
{
    public static ComponentStorage Create(ComponentID componentID) => throw new NotImplementedException();
}

