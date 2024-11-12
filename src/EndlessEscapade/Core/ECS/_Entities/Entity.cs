namespace EndlessEscapade.Core.ECS;

public struct Entity : IEntity, IEquatable<Entity>
{
	public readonly int Id;

	internal Entity(int id) {
		Id = id;
	}

	public override bool Equals(object? obj) {
		return obj is Entity entity && Equals(entity);
	}

    public override int GetHashCode() {
        return HashCode.Combine(Id);
    }

    public override string ToString() {
		return $"Id: {Id}";
	}

	public bool Equals(Entity other) {
		return other.Id == Id;
	}

	public T Get<T>() where T : struct {
		return ComponentSystem.Get<T>(Id);
	}

	public Entity Set<T>(T value) where T : struct {
        ComponentSystem.Set(Id, value);

        return this;
    }

	public bool Has<T>() where T : struct {
		return ComponentSystem.Has<T>(Id);
	}

	public bool Remove<T>() where T : struct {
		return ComponentSystem.Remove<T>(Id);
	}

    public bool Destroy() {
        return EntitySystem.Destroy(Id);
    }
}
