using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public interface IRenderLayer : IDisposable
{
    string Name { get; }
    
    RenderTarget2D Buffer { get; }
    
    List<RenderCallback> Callbacks { get; }
    
    SpriteBatchParameters Parameters { get; }
    
    void Fill();

    void Render();
}