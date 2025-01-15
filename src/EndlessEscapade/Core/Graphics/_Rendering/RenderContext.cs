using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public struct RenderContext
{
    /// <summary>
    ///     Gets or sets the sprite batch parameters used for rendering.
    /// </summary>
    public SpriteBatchParameters Parameters { get; set; }

    /// <summary>
    ///     Draws a sprite.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    public void Draw(in Sprite sprite)
    {
        Rendering.Draw(in this, in sprite);
    }

    /// <summary>
    ///     Draws a mesh.
    /// </summary>
    /// <param name="mesh">The mesh to draw.</param>
    public void Draw(in Mesh mesh)
    {
        Rendering.Draw(in this, in mesh);
    }
}