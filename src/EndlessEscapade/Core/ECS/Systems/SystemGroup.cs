using System.Collections.Generic;

namespace EndlessEscapade.Core.ECS.Systems;
internal class SystemGroup
{
    private SortedList<int, (ISystem System, Query Query)> _systems;
    private World _world;

    public SystemGroup(World world, params ISystem[] systems)
    {
        _world = world;
        _systems = new(systems.Length);
        foreach(var item in systems)
            _systems.Add(item.Order, (item, item.BuildQuery(world)));
    }

    public void Add(ISystem system) => _systems.Add(system.Order, (system, system.BuildQuery(_world)));

    public void Execute()
    {
        foreach ((_, (var system, var query)) in _systems)
        {
            system.Execute(query);
        }
    }
}
