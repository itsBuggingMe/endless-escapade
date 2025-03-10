using EndlessEscapade.Core.Collections;
using System.Collections.Immutable;

namespace EndlessEscapade.Core.ECS.Systems;

public class Query
{
    public World World { get; init; }
    private FastStack<Archetype> _appliesTo;
    private ImmutableArray<Rule> _rules;

    public Query(World world, ImmutableArray<Rule> rules)
    {
        World = world;
        world.OnArchetypeAdded += a =>
        {
            if(Applies(a, _rules.AsSpan()))
                _appliesTo.Push(a);
        };

        _rules = rules;

        foreach(var archetype in world.Archetypes)
        {
            if(Applies(archetype, rules.AsSpan()))
            {
                _appliesTo.Push(archetype);
            }
        }
    }

    public ReadOnlySpan<Archetype> Archetypes => _appliesTo.AsSpan();

    private static bool Applies(Archetype archetype, ReadOnlySpan<Rule> rules)
    {
        var indicies = archetype.IndexMap;
        foreach(var item in rules)
        {
            if(!item.Applies(indicies))
            {
                return false;
            }
        }
        return true;
    }
}
