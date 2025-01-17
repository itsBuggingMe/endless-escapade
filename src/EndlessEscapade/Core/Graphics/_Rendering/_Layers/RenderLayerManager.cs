using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class RenderLayerManager : ILoadable
{
    void ILoadable.Load(Mod mod) { }

    // TODO: Unload render layers
    void ILoadable.Unload() { }

    // TODO: Register render layers
    public static void Register<TRenderHook, TRenderLayer>(TRenderLayer layer) 
        where TRenderHook : RenderHook 
        where TRenderLayer : IRenderLayer
    {
        
    }
}