using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public struct Sprite
{
    /// <summary>
    /// 
    /// </summary>
    public Asset<Texture2D> Texture { readonly get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Vector2 Position { readonly get; set; }

    /// <summary>
    ///     
    /// </summary>
    public Vector2 Scale { readonly get; set; } = Vector2.One;
    
    /// <summary>
    ///     
    /// </summary>
    public Vector2 Origin { readonly get; set; } = new(0.5f);
    
    /// <summary>
    /// 
    /// </summary>
    public SpriteEffects Effects { readonly get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Rectangle? SourceRectangle { readonly get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Rectangle? DestinationRectangle { readonly get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public float Rotation { readonly get; set; }

    /// <summary>
    /// 
    /// </summary>
    public float Opacity
    {
        readonly get => _opacity;
        set => _opacity = MathHelper.Clamp(value, 0f, 1f);
    }

    /// <summary>
    /// 
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