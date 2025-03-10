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

    private DenseSet<EntityLocation> _table = new();
    private FastStack<(int Entity, int Version)> _recycledIDs = new FastStack<(int Entity, int Version)>();
    private int _nextID;

    public Entity SetComponents<T>(ref readonly T template, ArchetypeID archetypeID)
        where T : IEntityTemplate
    {
        (int id, int version) = _recycledIDs.TryPop(out var newId) ? newId : (_nextID++, 1);

        var archetype = _archetypes[archetypeID.GetRawValue()] ??= Archetype.Create(archetypeID);

        var index = archetype.Create();

        _table[id] = new EntityLocation(archetype, index, version);

        template.SetComponents(archetype, index);

        return new Entity(id, version, this);
    }

    public EntityTemplate Entity() => new() { World = this };

    internal struct EntityLocation(Archetype archetype, int index, int version)
    {
        internal Archetype Archetype = archetype;
        internal int Index = index;
        internal int Version = version;
    }
}
