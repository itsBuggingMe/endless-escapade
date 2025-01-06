using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public sealed class SpriteRendering 
{
    public static void Draw<T>(Asset<Texture2D> texture, Vector2 position, Color color) where T : RenderLayer
    {
        var layer = ModContent.GetInstance<T>();
        var sprite = new Sprite
        {
            Texture = texture,
            Position = position,
            Color = color
        };
        
        layer.Entries.Add(sprite);
    }
}