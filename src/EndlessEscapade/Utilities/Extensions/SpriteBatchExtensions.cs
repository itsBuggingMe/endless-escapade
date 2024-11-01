using System.Runtime.CompilerServices;
using EndlessEscapade.Core.Graphics;

namespace EndlessEscapade.Utilities.Extensions;

/// <summary>
///		Provides <see cref="SpriteBatch"/> extension methods.
/// </summary>
public static class SpriteBatchExtensions
{
    /// <summary>
    ///		Captures the current state of a <see cref="SpriteBatch"/> instance.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch"/> instance to capture.</param>
    /// <returns>The captured <see cref="SpriteBatchSnapshot"/> instance of the <see cref="SpriteBatch"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpriteBatchSnapshot Capture(this SpriteBatch spriteBatch) {
        return new SpriteBatchSnapshot(
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
    ///		Begins a <see cref="SpriteBatch"/> instance from a captured <see cref="SpriteBatchSnapshot"/> instance.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch"/> instance to begin.</param>
    /// <param name="snapshot">The <see cref="SpriteBatchSnapshot"/> instance to begin the <see cref="SpriteBatch"/> instance with.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Begin(this SpriteBatch spriteBatch, in SpriteBatchSnapshot snapshot) {
        spriteBatch.Begin(
            snapshot.SpriteSortMode,
            snapshot.BlendState,
            snapshot.SamplerState,
            snapshot.DepthStencilState,
            snapshot.RasterizerState,
            snapshot.Effect,
            snapshot.TransformMatrix
        );
    }
}
