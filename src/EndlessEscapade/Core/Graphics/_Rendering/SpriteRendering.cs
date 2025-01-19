using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public static class SpriteRendering
{
    private static SpriteBatch SpriteBatch => Main.spriteBatch;

    /// <summary>
    ///     Draws a sprite to the screen.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    /// <param name="parameters">The sprite batch parameters to use.</param>
    public static void Draw(in Sprite sprite, in SpriteBatchParameters parameters)
    {
        if (sprite.DestinationRectangle.HasValue)
        {
            SpriteBatch.Draw
            (
                sprite.Texture.Value,
                sprite.DestinationRectangle.Value,
                sprite.SourceRectangle,
                sprite.Color,
                sprite.Rotation,
                sprite.Origin,
                sprite.Effects,
                0f
            );
        }
        else
        {
            SpriteBatch.Draw
            (
                sprite.Texture.Value,
                sprite.Position,
                sprite.SourceRectangle,
                sprite.Color,
                sprite.Rotation,
                sprite.Origin,
                sprite.Scale,
                sprite.Effects,
                0f
            );
        }
    }
}