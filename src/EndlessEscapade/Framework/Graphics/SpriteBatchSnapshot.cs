namespace EndlessEscapade.Framework.Graphics;

public readonly ref struct SpriteBatchSnapshot
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
	/// <summary>
	///     The <see cref="SpriteSortMode" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly SpriteSortMode SpriteSortMode = spriteSortMode;

	/// <summary>
	///     The <see cref="BlendState" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly BlendState BlendState = blendState;

	/// <summary>
	///     The <see cref="SamplerState" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly SamplerState SamplerState = samplerState;

	/// <summary>
	///     The <see cref="DepthStencilState" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly DepthStencilState DepthStencilState = depthStencilState;

	/// <summary>
	///     The <see cref="RasterizerState" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly RasterizerState RasterizerState = rasterizerState;

	/// <summary>
	///     The <see cref="Effect" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly Effect Effect = effect;

	/// <summary>
	///     The <see cref="TransformMatrix" /> of the captured <see cref="SpriteBatch" /> instance.
	/// </summary>
	public readonly Matrix TransformMatrix = transformMatrix;
}