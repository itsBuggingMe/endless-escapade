using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public interface IRenderLayer : IDisposable
{
    string Name { get; }
    
    RenderTarget2D Buffer { get; }
    
    SpriteBatchParameters Parameters { get; }

    void Load();

    void Unload();
    
    void Fill();

    void Render();
}