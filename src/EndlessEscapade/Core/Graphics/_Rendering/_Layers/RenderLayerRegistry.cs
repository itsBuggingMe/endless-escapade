using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayerRegistry
{
    private readonly List<RenderLayer> layers = [];

    private readonly Dictionary<int, RenderLayer> layersById = [];
    private readonly Dictionary<string, RenderLayer> layersByName = [];
    
    public int Count { get; private set; }

    public RenderLayer Register(RenderLevel level, string name, bool isPixellated)
    {
        var id = Count;
        var layer = new RenderLayer(level, id, name, isPixellated);
        
        layers.Add(layer);
        
        if (layersById.ContainsKey(id))
        {
            throw new InvalidLayerException($"Layer {id} already exists.");
        }

        layersById[id] = layer;

        if (layersByName.ContainsKey(name))
        {
            throw new InvalidLayerException($@"Layer ""{name}"" already exists.");
        }
        
        layersByName[name] = layer;
        
        Count++;

        return layer;
    }

    public bool TryGet(string name, [MaybeNullWhen(false)] out RenderLayer layer)
    {
        return layersByName.TryGetValue(name, out layer);
    }

    public bool TryGet(int id, [MaybeNullWhen(false)] out RenderLayer layer)
    {
        return layersById.TryGetValue(id, out layer);
    }

    public RenderLayer Get(string name)
    {
        return layersByName[name] ?? throw new InvalidLayerException($@"Layer ""{name}"" does not exist.");
    }

    public RenderLayer Get(int id)
    {
        return layersById[id] ?? throw new InvalidLayerException($"Layer {id} does not exist.");
    }

    public IReadOnlyList<RenderLayer> Enumerate()
    {
        return layers;
    }
}