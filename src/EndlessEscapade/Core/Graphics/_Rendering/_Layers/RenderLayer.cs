using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayer : IRenderLayer
{
    public SpriteBatchParameters Parameters { get; set; }

    public List<Sprite> Entries { get; } = [];
    
    public float Depth { get; }
    
    public string Name { get; }
    
    public RenderLevel Level { get; }

    internal RenderLayer(RenderLevel level, string name, float depth)
    {
        Level = level;
        
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name;
        
        ArgumentOutOfRangeException.ThrowIfNegative(depth, nameof(depth));

        Depth = depth;
    }

    public void Draw(in Sprite sprite)
    {
        Entries.Add(sprite);
    }

    public void Render()
    {
        var spriteBatch = Main.spriteBatch;

        foreach (var entry in Entries)
        {
            if (entry.DestinationRectangle.HasValue)
            {
                spriteBatch.Draw
                (
                    entry.Texture.Value,
                    entry.DestinationRectangle.Value,
                    entry.SourceRectangle,
                    entry.Color,
                    entry.Rotation,
                    entry.Origin,
                    entry.Effects,
                    Depth
                );
            }
            else
            {
                spriteBatch.Draw
                (
                    entry.Texture.Value,
                    entry.Position,
                    entry.SourceRectangle,
                    entry.Color,
                    entry.Rotation,
                    entry.Origin,
                    entry.Scale,
                    entry.Effects,
                    Depth
                );
            }
        }
    }
}