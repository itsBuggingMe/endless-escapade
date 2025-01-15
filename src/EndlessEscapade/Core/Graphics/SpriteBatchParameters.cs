using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Graphics;

public struct SpriteBatchParameters 
(
    SpriteSortMode spriteSortMode,
    BlendState blendState,
    SamplerState samplerState,
    DepthStencilState depthStencilState,
    RasterizerState rasterizerState,
    Effect effect,
    Matrix transformMatrix
) 
{
    public SpriteSortMode SpriteSortMode { readonly get; set; } = spriteSortMode;
    
    public BlendState BlendState { readonly get; set; } = blendState;
    
    public SamplerState SamplerState { readonly get; set; } = samplerState;
    
    public DepthStencilState DepthStencilState { readonly get; set; } = depthStencilState;
    
    public RasterizerState RasterizerState { readonly get; set; } = rasterizerState;
    
    public Effect Effect { readonly get; set; } = effect;
    
    public Matrix TransformMatrix { readonly get; set; } = transformMatrix;
}