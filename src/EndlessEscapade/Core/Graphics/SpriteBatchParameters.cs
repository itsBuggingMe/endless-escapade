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
) : IComparable<SpriteBatchParameters>, IEquatable<SpriteBatchParameters>
{
    public SpriteSortMode SpriteSortMode { readonly get; set; } = spriteSortMode;

    public BlendState BlendState { readonly get; set; } = blendState;

    public SamplerState SamplerState { readonly get; set; } = samplerState;

    public DepthStencilState DepthStencilState { readonly get; set; } = depthStencilState;

    public RasterizerState RasterizerState { readonly get; set; } = rasterizerState;

    public Effect Effect { readonly get; set; } = effect;

    public Matrix TransformMatrix { readonly get; set; } = transformMatrix;

    public int CompareTo(SpriteBatchParameters other)
    {
        return Equals(other) ? 0 : -1;
    }
    
    public bool Equals(SpriteBatchParameters other)
    {
        return SpriteSortMode == other.SpriteSortMode
               && BlendState == other.BlendState
               && SamplerState == other.SamplerState
               && DepthStencilState == other.DepthStencilState
               && RasterizerState == other.RasterizerState
               && Effect == other.Effect
               && TransformMatrix == other.TransformMatrix;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is SpriteBatchParameters parameters && Equals(parameters);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, TransformMatrix);
    }

    public static bool operator ==(SpriteBatchParameters left, SpriteBatchParameters right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SpriteBatchParameters left, SpriteBatchParameters right)
    {
        return !left.Equals(right);
    }
}