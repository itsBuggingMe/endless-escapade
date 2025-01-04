using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public struct Sprite
{
    public Asset<Texture2D> Texture { get; set; }
    
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; } = new(0.5f);
    
    public SpriteEffects Effects { get; set; }
    
    public Rectangle? SourceRectangle { get; set; }
    public Rectangle? DestinationRectangle { get; set; }
    
    public float Opacity
    {
        readonly get => _opacity;
        set => _opacity = MathHelper.Clamp(value, 0f, 1f);
    }

    public Color Color
    {
        readonly get => _color;
        set => _color = value * Opacity;
    }

    private float _opacity = 1f;

    private Color _color = Color.White;
    
    public Sprite() { }
}