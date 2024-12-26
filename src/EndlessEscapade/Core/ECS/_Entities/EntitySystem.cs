using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.ECS;

public sealed class EntitySystem : ModSystem
{
    /// <summary>
    ///     Invoked every time an entity is created.
    /// </summary>
    public static event Action<Entity> OnEntityCreated;

    /// <summary>
    ///     Invoked every time an entity is destroyed.
    /// </summary>
    public static event Action<Entity> OnEntityDestroyed;
    
    private static readonly Queue<int> Indices = [];

    private static Entity[] entities = [];

    private static int nextEntityId;

    public override void Unload()
    {
        base.Unload();

        OnEntityCreated = null;
        OnEntityDestroyed = null;
    }

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

        OnEntityCreated?.Invoke(entity);

        return entity;
    }

    public static bool Destroy(int entityId)
    {
        if (entityId < 0 || entityId >= entities.Length)
        {
            return false;
        }

        for (var i = 0; i < ComponentSystem.ComponentTypeCount; i++)
        {
            var masks = MathUtils.DivCeil(ComponentSystem.ComponentTypeCount, ComponentSystem.MaskSize);
            var index = entityId * masks + Math.DivRem(i, ComponentSystem.MaskSize, out var remainder);

            var mask = 1UL << remainder;

            ComponentSystem.Flags[index] &= ~mask;
        }

        OnEntityDestroyed?.Invoke(entities[entityId]);

        return true;
    }
}