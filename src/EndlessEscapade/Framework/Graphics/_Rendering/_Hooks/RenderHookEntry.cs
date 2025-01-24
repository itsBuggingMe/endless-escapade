using System.Collections.Generic;

namespace EndlessEscapade.Framework.Graphics;

public sealed class RenderHookEntry
{
    private List<IRenderElement> Elements { get; } = new();
    
    /// <summary>
    ///     Gets or sets the sprite batch parameters used for rendering.
    /// </summary>
    public SpriteBatchParameters Parameters = new
    (
        SpriteSortMode.Deferred,
        BlendState.NonPremultiplied, 
        SamplerState.PointClamp,
        default,
        Main.Rasterizer,
        default, 
        Main.GameViewMatrix.TransformationMatrix
    );

    public RenderHookEntry() { }

    /// <summary>
    ///     Queues a render element to be drawn to the screen.
    /// </summary>
    /// <param name="element">The render element to queue.</param>
    /// <typeparam name="TRenderElement">The type of the render element to queue.</typeparam>
    public void Queue<TRenderElement>(in TRenderElement element) where TRenderElement : IRenderElement
    {
        Elements.Add(element);
    }

    /// <summary>
    ///     Draws all queued render elements to the screen.
    /// </summary>
    /// <param name="parameters">The sprite batch parameters to use.</param>
    public void Draw(in SpriteBatchParameters parameters)
    {
        foreach (var element in Elements)
        {
            element.Draw(in parameters);
        }
    }
    
     /// <summary>
    ///     Sets the sprite sort mode for the render entry.
    /// </summary>
    /// <param name="spriteSortMode">The sprite sort mode to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseSpriteSortMode(SpriteSortMode spriteSortMode)
    {
        Parameters.SpriteSortMode = spriteSortMode;
        
        return this;
    }

    /// <summary>
    ///     Sets the blend state for the render entry.
    /// </summary>
    /// <param name="blendState">The blend state to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseBlendState(BlendState blendState)
    {
        Parameters.BlendState = blendState;
        
        return this;
    }

    /// <summary>
    ///     Sets the sampler state for the render entry.
    /// </summary>
    /// <param name="samplerState">The sampler state to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseSamplerState(SamplerState samplerState)
    {
        Parameters.SamplerState = samplerState;
        
        return this;
    }

    /// <summary>
    ///     Sets the depth stencil state for the render entry.
    /// </summary>
    /// <param name="depthStencilState">The depth stencil state to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseDepthStencilState(DepthStencilState depthStencilState)
    {
        Parameters.DepthStencilState = depthStencilState;
        
        return this;
    }

    /// <summary>
    ///     Sets the rasterizer state for the render entry.
    /// </summary>
    /// <param name="rasterizerState">The rasterizer state to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseRasterizerState(RasterizerState rasterizerState)
    {
        Parameters.RasterizerState = rasterizerState;
        
        return this;
    }

    /// <summary>
    ///     Sets the effect for the render entry.
    /// </summary>
    /// <param name="effect">The effect to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseEffect(Effect effect)
    {
        Parameters.Effect = effect;
        
        return this;
    }

    /// <summary>
    ///     Sets the transformation matrix for the render entry.
    /// </summary>
    /// <param name="transformMatrix">The transformation matrix to use.</param>
    /// <returns>The updated <see cref="RenderHookEntry"/>.</returns>
    public RenderHookEntry UseTransformMatrix(Matrix transformMatrix)
    {
        Parameters.TransformMatrix = transformMatrix;
        
        return this;
    }
}