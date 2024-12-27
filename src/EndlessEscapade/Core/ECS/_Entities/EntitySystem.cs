using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EndlessEscapade.Utilities;
using Terraria.DataStructures;

namespace EndlessEscapade.Core.ECS;

public sealed class EntitySystem : ModSystem
{
    private static readonly Queue<int> Indices = [];

    private static Entity[] entities = [];

    private static int nextEntityId;

    public static Entity Create()
    {
        int id;

        if (!Indices.TryDequeue(out id))
        {
            id = nextEntityId++;
        }

        ArrayUtils.EnsureCapacity(ref entities, id);

        var entity = new Entity(id);

        entities[id] = entity;

        return entity;
    }

    public static bool Destroy(int id)
    {
        if (!TryGet(id, out var entity))
        {
            return false;
        }

        for (var i = 0; i < ComponentSystem.ComponentTypeCount; i++)
        {
            var masks = MathUtils.DivCeil(ComponentSystem.ComponentTypeCount, ComponentSystem.MaskSize);
            var index = id * masks + Math.DivRem(i, ComponentSystem.MaskSize, out var remainder);

            var mask = 1UL << remainder;

            ComponentSystem.Flags[index] &= ~mask;
        }

        return true;
    }

    public static bool TryGet(int id, out Entity entity)
    {
        entity = default;
        
        if (id < 0 || id >= entities.Length)
        {
            return false;
        }

        entity = entities[id];
        
        return true;
    }
}