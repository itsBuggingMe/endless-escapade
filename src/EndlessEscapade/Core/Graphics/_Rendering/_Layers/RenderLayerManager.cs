using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Graphics;

public static class RenderLayerManager
{
    private static RenderLayerRegistry registry = new();

    public static int Count => registry.Count;

    public static RenderLayer Register(RenderLevel level, string name, bool isPixellated)
    {
        return registry.Register(level, name, isPixellated);
    }
    
    public static bool TryGet(string name, [MaybeNullWhen(false)] out RenderLayer layer)
    {
        return registry.TryGet(name, out layer);
    }

    public static bool TryGet(int id, [MaybeNullWhen(false)] out RenderLayer layer)
    {
        return registry.TryGet(id, out layer);
    }

    public static RenderLayer Get(string name)
    {
        return registry.Get(name);
    }

    public static RenderLayer Get(int id)
    {
        return registry.Get(id);
    }
    
    public static IReadOnlyList<RenderLayer> Enumerate()
    {
        return registry.Enumerate();
    }

    internal static void Unload()
    {
        Main.QueueMainThreadAction(() =>
        {
            foreach (var layer in registry.Enumerate())
            {
                layer.Dispose();
            }
            registry.Clear();
            registry = null!;
        });
    }
}