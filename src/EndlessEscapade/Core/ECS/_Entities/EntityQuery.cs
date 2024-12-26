using System.Collections.Generic;

namespace EndlessEscapade.Core.ECS;

public static class EntityQuery<T> where T : struct
{
    private static readonly List<Entity> Entities = [];
    
    static EntityQuery()
    {
    }
    
    
}