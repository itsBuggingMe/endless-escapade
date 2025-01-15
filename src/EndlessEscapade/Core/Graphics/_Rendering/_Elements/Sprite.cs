using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public struct Sprite
{
    /// <summary>
    ///     Gets or sets the texture of the sprite, as an asset.
    /// </summary>
    public Asset<Texture2D> Texture { readonly get; set; }

    /// <summary>
    ///     Gets or sets the position of the sprite, in screen coordinates.
    /// </summary>
    public Vector2 Position { readonly get; set; }

    /// <summary>
    ///     Gets or sets the scale of the sprite. Defaults to <see cref="Vector2.One"/>.
    /// </summary>
    public Vector2 Scale { readonly get; set; } = Vector2.One;
    
    /// <summary>
    ///     Gets or sets the origin of the sprite.
    /// </summary>
    public Vector2 Origin { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the effects of the sprite.
    /// </summary>
    public SpriteEffects Effects { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the source rectangle of the sprite.
    /// </summary>
    public Rectangle? SourceRectangle { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the destination rectangle of the sprite.
    /// </summary>
    public Rectangle? DestinationRectangle { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the rotation of the sprite, in radians.
    /// </summary>
    public float Rotation { readonly get; set; }

    /// <summary>
    ///     Gets or sets the opacity of the sprite, where 0 is fully transparent and 1 is fully opaque.
    /// </summary>
    /// <remarks>
    ///     This value is clamped between 0 and 1.
    /// </remarks>
    public float Opacity
    {
        readonly get => _opacity;
        set => _opacity = MathHelper.Clamp(value, 0f, 1f);
    }

    /// <summary>
    ///     Gets or sets the color of the sprite. Defaults to <see cref="Color.White"/>.
    /// </summary>
    public Color Color
    {
        readonly get => _color;
        set => _color = value * Opacity;
    }

    private float _opacity = 1f;

    private Color _color = Color.White;
    
    public Sprite() { }
}