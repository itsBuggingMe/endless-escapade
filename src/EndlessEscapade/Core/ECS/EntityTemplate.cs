using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessEscapade.Core.Collections._Generic;

namespace EndlessEscapade.Core.ECS;

public struct Add<TRest, T> : IEntityTemplate
    where TRest : IEntityTemplate
{
    public readonly ArchetypeID ArchetypeID => cache ??= IEntityTemplate.CalculateArchetypeID<EntityTemplate<T>>();
    private static ArchetypeID? cache;


    public World World => Rest.World;
    public Entity Entity => World.SetComponents(ref this, ArchetypeID);


    public TRest Rest;
    public T Value;


    public Add<Add<TRest, T>, TN> Set<TN>(TN value) => new()
    { 
        Rest = this, 
        Value = value 
    };

    public void AppendTypes(ref FastStack<Type> types)
    {
        types.Push(typeof(TRest));
        Rest.AppendTypes(ref types);
    }

    public void SetComponents(Archetype archetype, int index)
    {
        archetype.GetComponent<T>(index) = Value;
        Rest.SetComponents(archetype, index);
    }
}

public struct EntityTemplate<T> : IEntityTemplate
{
    public readonly ArchetypeID ArchetypeID => cache ??= IEntityTemplate.CalculateArchetypeID<EntityTemplate<T>>();
    private static ArchetypeID? cache;

    public required World World { get; init; }
    public Entity Entity => World.SetComponents(ref this, ArchetypeID);


    public T Value;


    public Add<EntityTemplate<T>, TN> Set<TN>(TN value) => new()
    {
        Rest = this,
        Value = value,
    };

    public void AppendTypes(ref FastStack<Type> types) => types.Push(typeof(T));

    public void SetComponents(Archetype archetype, int index) => archetype.GetComponent<T>(index) = Value;
}

public struct EntityTemplate
{
    public World World;
    public EntityTemplate<T> Set<T>(T value) => new()
    {
        World = World,
        Value = value,
    };
}

public interface IEntityTemplate
{
    public void AppendTypes(ref FastStack<Type> types);
    public void SetComponents(Archetype archetype, int index);
    public Entity Entity { get; }
    public ArchetypeID ArchetypeID { get; }
    public World World { get; }

    static ArchetypeID CalculateArchetypeID<T>()
        where T : struct, IEntityTemplate
    {
        ref FastStack<Type> types = ref sharedStack;
        default(T)!.AppendTypes(ref types);
        var id = Archetype.GetArchetypeID(types.AsSpan());
        types.Clear();
        return id;
    }

    [ThreadStatic]
    private static FastStack<Type> sharedStack = new();
}