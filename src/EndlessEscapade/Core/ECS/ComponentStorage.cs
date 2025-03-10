using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;

public abstract class ComponentStorage
{
    public abstract void Delete(int archetypeID);
}

public class ComponentStorage<T> : ComponentStorage
{
    public ref T this[int index]
    {
        get => throw new NotImplementedException();
    }
}