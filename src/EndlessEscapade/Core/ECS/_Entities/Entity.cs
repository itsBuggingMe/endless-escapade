namespace EndlessEscapade.Core.ECS;

public struct Entity : IEntity
{
    public readonly int Id;

    internal Entity(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"Id: {Id}";
    }

    public ref T Get<T>() where T : struct
    {
        return ref ComponentSystem.Get<T>(Id);
    }

    public Entity Set<T>(T value) where T : struct
    {
        ComponentSystem.Set(Id, value);

        return this;
    }

    public bool Has<T>() where T : struct
    {
        return ComponentSystem.Has<T>(Id);
    }

    public bool Remove<T>() where T : struct
    {
        return ComponentSystem.Remove<T>(Id);
    }

    public bool Destroy()
    {
        return EntitySystem.Destroy(Id);
    }
}