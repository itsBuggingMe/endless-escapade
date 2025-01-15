using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public struct RenderContext
{
    public SpriteBatchParameters Parameters { get; set; }

    public void Draw(in Sprite sprite)
    {
        Rendering.Draw(in this, in sprite);
    }

    public void Draw(in Mesh mesh)
    {
        Rendering.Draw(in this, in mesh);
    }
}