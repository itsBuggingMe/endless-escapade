namespace EndlessEscapade.Framework.Graphics;

public interface IRenderElement
{
    /// <summary>
    ///     Draws the render element to the screen.
    /// </summary>
    /// <param name="parameters">The sprite batch parameters to use.</param>
    void Draw(in SpriteBatchParameters parameters);
}