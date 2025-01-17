using EndlessEscapade.Utilities;
using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public struct RenderContext
{
    private static SpriteBatch SpriteBatch => Main.spriteBatch;
    
    /// <summary>
    ///     Gets or sets the sprite batch parameters used for rendering.
    /// </summary>
    public SpriteBatchParameters Parameters = new();

    public RenderContext() { }

    /// <summary>
    ///     Draws a sprite.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    public void Draw(in Sprite sprite)
    {
        SpriteRendering.Draw(in sprite);
    }

    /// <summary>
    ///     Draws a mesh.
    /// </summary>
    /// <param name="mesh">The mesh to draw.</param>
    public void Draw(in Mesh mesh)
    {
        MeshRendering.Draw(in mesh, Parameters.Effect);
    }

    public void Begin()
    {
        SpriteBatch.Begin(in Parameters);
    }

    public void End()
    {
        SpriteBatch.End();
    }

    public RenderContext UseSpriteSortMode(SpriteSortMode spriteSortMode)
    {
        Parameters.SpriteSortMode = spriteSortMode;
        
        return this;
    }

    public RenderContext UseBlendState(BlendState blendState)
    {
        Parameters.BlendState = blendState;
        
        return this;
    }

    public RenderContext UseSamplerState(SamplerState samplerState)
    {
        Parameters.SamplerState = samplerState;
        
        return this;
    }

    public RenderContext UseDepthStencilState(DepthStencilState depthStencilState)
    {
        Parameters.DepthStencilState = depthStencilState;
        
        return this;
    }

    public RenderContext UseRasterizerState(RasterizerState rasterizerState)
    {
        Parameters.RasterizerState = rasterizerState;
        
        return this;
    }

    public RenderContext UseEffect(Effect effect)
    {
        Parameters.Effect = effect;
        
        return this;
    }

    public RenderContext UseTransformMatrix(Matrix transformMatrix)
    {
        Parameters.TransformMatrix = transformMatrix;
        
        return this;
    }
}