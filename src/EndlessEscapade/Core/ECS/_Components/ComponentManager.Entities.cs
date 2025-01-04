namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentManager : ModSystem
{
    private static class ComponentEntityData<T> where T : struct
    {
        public static readonly ComponentRegistry<T> Registry = new(EntityManager.INITIAL_ENTITY_COUNT);
    }

    public static ref T Get<T>(int entityId) where T : struct
    {
        if (!ComponentEntityData<T>.Registry.Has(entityId))
        {
            throw new ComponentNotFoundException($"Entity {entityId} does not have a component of type {typeof(T).FullName}");
        }

        return ref ComponentEntityData<T>.Registry.Get(entityId);
    }

    public static void Set<T>(int entityId, T value) where T : struct
    {
        if (!EntityManager.Has(entityId))
        {
            throw new InvalidEntityException($"Entity {entityId} does not exist.");
        }

        ComponentEntityData<T>.Registry.Set(entityId, value);

        OnEntityComponentAdded?.Invoke(EntityManager.Get(entityId));
    }

    public static bool Remove<T>(int entityId) where T : struct
    {
        if (!ComponentEntityData<T>.Registry.Remove(entityId))
        {
            return false;
        }

        OnEntityComponentRemoved?.Invoke(EntityManager.Get(entityId));

        return true;
    }

    public static bool Has<T>(int entityId) where T : struct
    {
        return ComponentEntityData<T>.Registry.Has(entityId);
    }
}