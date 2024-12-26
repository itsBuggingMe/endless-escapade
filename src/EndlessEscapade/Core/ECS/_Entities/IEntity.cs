namespace EndlessEscapade.Core.ECS;

public interface IEntity
{
    T Get<T>() where T : struct;

    Entity Set<T>(T value) where T : struct;

    bool Has<T>() where T : struct;

    bool Remove<T>() where T : struct;
}