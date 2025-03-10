namespace EndlessEscapade.Core.ECS;

public readonly struct Entity(int entityID, int entityVersion, World world)
{
    public readonly int EntityID = entityID;
    public readonly int EntityVersion = entityVersion;
    public readonly World World = world;

    public readonly bool Has<T>()
    {
        var location = World.Table[EntityID];
        return location.Version != EntityVersion && location.Archetype.HasComponent<T>(out _);
    }

    public readonly ref T Get<T>()
    {
        var location = World.Table[EntityID];
        if(location.Version != EntityVersion)
            ThrowHelper.Throw_InvalidOperation(EntityIsDead);
        return ref location.Archetype.GetComponent<T>(location.Index);
    }

    public readonly bool TryGet<T>(out Ref<T> item)
    {
        var location = World.Table[EntityID];
        if(location.Version != EntityVersion)
            goto noComponent;

        if(!location.Archetype.HasComponent<T>(out int storageIndex))
            goto noComponent;

        item = new(ref location.Archetype.GetComponentKnownComponentStorageIndex<T>(
            location.Index,
            storageIndex));

        return true;

        noComponent:
        item = default!;
        return false;
    }

    public bool Delete() => World.Delete(this);

    private const string EntityIsDead = "Entity is Dead";
}
