using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayerRegistry : IDisposable
{
    private Dictionary<string, RenderLayer> layersByName = [];

    public void Register<T>(string name, T layer) where T : RenderLayer
    {
        if (layersByName.ContainsKey(name))
        {
            throw new InvalidRenderLayerException($@"Layer ""{name}"" already exists.");
        }
        
        layer.Subscribe();
        
        layersByName[name] = layer;
    }

    public bool TryGet(string name, [MaybeNullWhen(false)] out RenderLayer layer)
    {
        return layersByName.TryGetValue(name, out layer);
    }

    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {

        }
        
        layersByName?.Clear();
        layersByName = null;
    }
}