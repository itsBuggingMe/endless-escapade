using EndlessEscapade.Core.Collections._Generic;
using EndlessEscapade.Framework.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS;

public class World
{
    //archetypeID -> Archetype
    private SparseSet<Archetype> _archetypes = new();

    private Dictionary<(bool IsAddAction, Type Delta, Archetype Archetype), Archetype> _archetypeGraph = [];
    private Dictionary<ImmutableHashSet<Type>, HashSet<Archetype>> QueryCache = [];

    private DenseSet<EntityLocation> _table = new();
    private FastStack<(int Entity, int Version)> _recycledIDs = new FastStack<(int Entity, int Version)>();
    private int _nextID;

    public Entity Create<T>(in T tuple)
        where T : struct, IRec
    {
        (int id, int version) = _recycledIDs.TryPop(out var newId) ? newId : (_nextID++, 1);

        var toArchetypeID = tuple.ArchetypeID;
        var archetype = _archetypes[toArchetypeID.GetRawValue()] ??= Archetype.Create(toArchetypeID);

        int index = archetype.Create();

        _table[id] = new EntityLocation(archetype, id, version);

        //set components
        tuple.SetArchetype(archetype, index);

        return new Entity(id, version, this);
    }

    internal struct EntityLocation(Archetype archetype, int entityID, int version)
    {
        internal Archetype Archetype = archetype;
        internal int EntityID = entityID;
        internal int Version = version;
    }
}
