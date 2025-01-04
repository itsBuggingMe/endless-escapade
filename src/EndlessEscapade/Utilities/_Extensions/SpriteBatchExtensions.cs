using System.Runtime.CompilerServices;
using EndlessEscapade.Core.Graphics;

namespace EndlessEscapade.Utilities;

/// <summary>
///     Provides <see cref="SpriteBatch" /> extension methods.
/// </summary>
public static class SpriteBatchExtensions
{
    /// <summary>
    ///     Captures the current state of a <see cref="SpriteBatch" /> instance.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch" /> instance to capture.</param>
    /// <returns>The captured <see cref="SpriteBatchParameters" /> instance of the <see cref="SpriteBatch" /> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpriteBatchParameters Capture(this SpriteBatch spriteBatch)
    {
        return new SpriteBatchParameters
        (
            spriteBatch.sortMode,
            spriteBatch.blendState,
            spriteBatch.samplerState,
            spriteBatch.depthStencilState,
            spriteBatch.rasterizerState,
            spriteBatch.spriteEffect,
            spriteBatch.transformMatrix
        );
    }

    /// <summary>
    ///     Begins a <see cref="SpriteBatch" /> instance from a captured <see cref="SpriteBatchParameters" /> instance.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch" /> instance to begin.</param>
    /// <param name="parameters">
    ///     The <see cref="SpriteBatchParameters" /> instance to begin the <see cref="SpriteBatch" /> instance
    ///     with.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Begin(this SpriteBatch spriteBatch, in SpriteBatchParameters parameters)
    {
        spriteBatch.Begin
        (
            parameters.SpriteSortMode,
            parameters.BlendState,
            parameters.SamplerState,
            parameters.DepthStencilState,
            parameters.RasterizerState,
            parameters.Effect,
            parameters.TransformMatrix
        );
    }
}