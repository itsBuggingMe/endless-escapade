namespace EndlessEscapade.Framework;

public readonly struct Entity
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

    public void Set<T>(T value) where T : struct
    {
        ComponentSystem.Set(Id, value);
    }

    public bool Has<T>() where T : struct
    {
        return ComponentSystem.Has<T>(Id);
    }

    public bool Remove<T>() where T : struct
    {
        return ComponentSystem.Remove<T>(Id);
    }
}