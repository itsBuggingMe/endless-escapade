using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.ECS;

public readonly struct Entity : IEquatable<Entity>
{
    public readonly int Id;

    internal Entity(int id)
    {
        Id = id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
    
    public override string ToString()   
    {
        return $"Id: {Id}";
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Entity entity && Equals(entity);
    }

    public ref T Get<T>() where T : struct
    {
        return ref ComponentManager.Get<T>(Id);
    }

    public void Set<T>(T value) where T : struct
    {
        ComponentManager.Set(Id, value);
    }
    
    public bool Remove<T>() where T : struct
    {
        return ComponentManager.Remove<T>(Id);
    }

    public bool Has<T>() where T : struct
    {
        return ComponentManager.Has<T>(Id);
    }

    public bool Equals(Entity other)
    {
        return other.Id == Id;
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return left.Id == right.Id;
    }
    
    public static bool operator !=(Entity left, Entity right)
    {
        return left.Id != right.Id;
    }
}