using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public struct SpriteContainer
{
    public List<Sprite> Entries { get; private set; } = [];
    
    public SpriteContainer() { }
}