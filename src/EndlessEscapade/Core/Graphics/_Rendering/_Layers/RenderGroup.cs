using System.Collections;
using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public readonly struct RenderGroup : IEnumerable<IRenderLayer>
{
    private readonly List<IRenderLayer> layers = [];

    public RenderGroup() { }

    public readonly IEnumerator<IRenderLayer> GetEnumerator()
    {
        return layers.GetEnumerator();
    }

    readonly IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal readonly void Add<T>(T layer) where T : IRenderLayer
    {
        layers.Add(layer);
    }
}