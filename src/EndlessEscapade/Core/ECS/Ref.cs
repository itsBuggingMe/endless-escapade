using EndlessEscapade.Core.Collections._Generic;
using EndlessEscapade.Framework.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EndlessEscapade.Core.ECS;

public ref struct Ref<T>
{
    public ref T Ref;
}