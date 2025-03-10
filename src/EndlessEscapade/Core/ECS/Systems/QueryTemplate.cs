using EndlessEscapade.Core.Collections;
using EndlessEscapade.Core.ECS.Data;
using System.Collections.Immutable;

namespace EndlessEscapade.Core.ECS.Systems;

public struct QueryBuilder(World world)
{
    public World World { get; } = world;

    private FastStack<Rule> _rules = new();

    public QueryBuilder With<T>()
    {
        _rules.Push(new Rule(Rule.RuleType.Include, Component<T>.ID));
        return this;
    }

    public QueryBuilder Without<T>()
    {
        _rules.Push(new Rule(Rule.RuleType.Exclude, Component<T>.ID));
        return this;
    }

    public Query Build()
    {
        return new Query(World, _rules.AsSpan().ToImmutableArray());
    }
}