using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EndlessEscapade.Utilities;
using Terraria.DataStructures;

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

    public static bool Destroy(int id)
    {
        if (id < 0 || id >= entities.Length)
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

        OnEntityDestroyed?.Invoke(entities[id]);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Entity Get(int id)
    {
        return entities[id];
    }
}