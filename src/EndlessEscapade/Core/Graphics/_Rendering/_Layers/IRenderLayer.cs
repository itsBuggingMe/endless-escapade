using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public interface IRenderLayer : ISpriteBatch
{
    SpriteBatchParameters Parameters { get; }
    
    List<Sprite> Entries { get; }
    
    string Name { get; }
    
    float Depth { get; }
    
    RenderLevel Level { get; }

    void Render();
}