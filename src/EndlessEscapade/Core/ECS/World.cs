using EndlessEscapade.Core.Collections._Generic;
using EndlessEscapade.Framework.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;

public class World
{
    //archetypeID -> Archetype
    private SparseSet<Archetype> _archetypes = new();

    private Dictionary<(bool IsAddAction, Type Delta, Archetype Archetype), Archetype> _archetypeGraph = [];
    private Dictionary<ImmutableHashSet<Type>, HashSet<Archetype>> QueryCache = [];

    internal DenseSet<EntityLocation> Table = new();
    private FastStack<EntityLight> _recycledIDs = new FastStack<EntityLight>();
    private int _nextID;

    public Entity SetComponents<T>(ref readonly T template, ArchetypeID archetypeID)
        where T : IEntityTemplate
    {
        (int id, int version) = _recycledIDs.TryPop(out var newId) ? newId : (_nextID++, 1);

        var archetype = _archetypes[archetypeID.GetRawValue()] ??= Archetype.Create(archetypeID);

        var index = archetype.Create();

        Table[id] = new EntityLocation(archetype, index, version);

        template.SetComponents(archetype, index);

        return new Entity(id, version, this);
    }

    public EntityTemplate Entity() => new() { World = this };

    public bool Delete(Entity entity)
    {
        ref var location = ref Table[entity.EntityID];
        if(location.Version != entity.EntityVersion)
            return false;
        foreach(var storage in location.Archetype.Storages)
            storage.Delete(location.Index);
        throw new NotImplementedException();
        _recycledIDs.Push(new(entity.EntityID, entity.EntityVersion));
        location = EntityLocation.Default;
    }

    internal struct EntityLocation(Archetype archetype, int index, int version)
    {
        internal Archetype Archetype = archetype;
        internal int Index = index;
        internal int Version = version;

        public static EntityLocation Default = new EntityLocation(null!, -1, -1);
    }
}
