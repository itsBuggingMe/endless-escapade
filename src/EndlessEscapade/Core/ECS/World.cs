using EndlessEscapade.Core.Collections;
using EndlessEscapade.Core.ECS.Data;
using EndlessEscapade.Core.ECS.Systems;
using EndlessEscapade.Framework.Collections;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace EndlessEscapade.Core.ECS;

public class World
{
    public Action<Archetype>? OnArchetypeAdded;

    //archetypeID -> Archetype
    private SparseSet<Archetype> _archetypes = new();

    private Dictionary<ArchetypeGraphEdge, Archetype> _archetypeGraph = [];
    private Dictionary<ImmutableHashSet<Type>, HashSet<Archetype>> QueryCache = [];

    internal DenseSet<EntityLocation> Table = new();
    private FastStack<EntityLight> _recycledIDs = new FastStack<EntityLight>();
    private int _nextID;

    public Entity SetComponents<T>(ref readonly T template, ArchetypeID archetypeID)
        where T : IEntityTemplate
    {
        (int id, int version) = _recycledIDs.TryPop(out var newId) ? newId : new(_nextID++, 1);

        var archetype = _archetypes[archetypeID.GetRawValue()] ??= InvokeArchetypeEvents(Archetype.Create(archetypeID));

        var index = archetype.Create(out var entitySlot);
        entitySlot.R.ID = id;
        entitySlot.R.Version = id;

        Table[id] = new EntityLocation(archetype, index, version);

        template.SetComponents(archetype, index);

        return new Entity(id, version, this);
    }

    public EntityTemplate Entity() => new() { World = this };

    public bool Add<T>(Entity entity, in T component)
    {
        Debug.Assert(entity.World == this);

        ref EntityLocation location = ref Table[entity.EntityID];
        if (location.Archetype.HasComponent<T>(out _) || location.Version != entity.EntityVersion)
            return false;

        var destination = CollectionsMarshal.GetValueRefOrAddDefault(_archetypeGraph, new ArchetypeGraphEdge(true, Component<T>.ID, location.Archetype), out _)
            ??= FindAdjacientArchetypeCold(true, location.Archetype, Component<T>.ID);

        MoveEntityTo(entity, ref location, location.Archetype, destination);

        destination.GetComponent<T>(location.Index) = component;

        var world = this;

        return true;
    }

    public bool Remove<T>(Entity entity)
    {
        Debug.Assert(entity.World == this);

        ref EntityLocation location = ref Table[entity.EntityID];
        if (!location.Archetype.HasComponent<T>(out _) || location.Version != entity.EntityVersion)
            return false;

        var destination = CollectionsMarshal.GetValueRefOrAddDefault(_archetypeGraph, new ArchetypeGraphEdge(false, Component<T>.ID, location.Archetype), out _)
            ??= FindAdjacientArchetypeCold(false, location.Archetype, Component<T>.ID);

        location.Archetype.GetComponentStorage<T>().Delete(location.Index, location.Archetype.Capacity);
        MoveEntityTo(entity, ref location, location.Archetype, destination);


        return true;
    }

    private Archetype FindAdjacientArchetypeCold(bool isAdding, Archetype from, ComponentID type)
    {
        var types = from.ID.ComponentIDs.AsSpan();

        ArchetypeID nextId;

        if (isAdding)
        {
            Span<ComponentID> newIds = stackalloc ComponentID[types.Length + 1];
            types.CopyTo(newIds[..^1]);
            newIds[^1] = type;
            nextId = Archetype.GetArchetypeID(newIds);
        }
        else
        {
            Span<ComponentID> newIds = stackalloc ComponentID[types.Length - 1];
            int index = 0;
            foreach (var id in types)
                if (id != type)
                    newIds[index++] = id;
            Debug.Assert(index == newIds.Length);
            nextId = Archetype.GetArchetypeID(newIds);
        }

        return _archetypes[nextId.GetRawValue()] ??= InvokeArchetypeEvents(Archetype.Create(nextId));
    }

    private Archetype InvokeArchetypeEvents(Archetype archetype)
    {
        OnArchetypeAdded?.Invoke(archetype);
        return archetype;
    }

    /// <summary>
    /// Moves the components of an entity from <paramref name="from"/> to <paramref name="destination"/>. 
    /// <para>If a component type is not in <paramref name="destination"/>, it is not updated and needs to be done manually.</para>
    /// <para>If a component type is not in <paramref name="from"/>, the destination skips it. </para>
    /// </summary>
    private void MoveEntityTo(Entity entity, ref EntityLocation location, Archetype from, Archetype destination)
    {
        var fromIndex = location.Index;
        var fromTop = location.Archetype.Count;
        var destIndex = destination.Create(out var slot);
        
        foreach (var storage in destination.Storages)
        {
            storage.Pull(location.Archetype, fromIndex, destIndex, fromTop);
        }

        //update entity records

        var movedEntity = from.DeleteFromEntityStorageOnly(fromIndex);

        ref var movedEntityLocation = ref Table[movedEntity.ID];

        movedEntityLocation = location;
        location.Archetype = destination;
        location.Index = destIndex;

        slot.R.ID = entity.EntityID;
        slot.R.Version = entity.EntityVersion;


    }

    public bool Delete(Entity entity)
    {
        ref var location = ref Table[entity.EntityID];
        if(location.Version != entity.EntityVersion)
            return false;

        var deletedEntity = location.Archetype.Delete(location.Index);

        ref var movedDownEntityLocation = ref Table[deletedEntity.ID];
        movedDownEntityLocation = location;
        location = EntityLocation.Default;

        _recycledIDs.Push(new(entity.EntityID, entity.EntityVersion));

        return true;
    }

    public QueryBuilder Query() => new(this);

    internal struct EntityLocation(Archetype archetype, int index, int version)
    {
        internal Archetype Archetype = archetype;
        internal int Index = index;
        internal int Version = version;

        public static EntityLocation Default = new EntityLocation(null!, -1, -1);
    }

    public ReadOnlySpan<Archetype> Archetypes => _archetypes.AsSpan();

    internal record struct ArchetypeGraphEdge(bool IsAddAction, ComponentID Delta, Archetype Archetype);
}
