using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public struct Sprite : IRenderElement
{
    /// <summary>
    ///     Gets or sets the texture of the sprite, as an asset.
    /// </summary>
    public Asset<Texture2D> Texture;

    /// <summary>
    ///     Gets or sets the position of the sprite, in screen coordinates.
    /// </summary>
    public Vector2 Position;

    /// <summary>
    ///     Gets or sets the scale of the sprite. Defaults to <see cref="Vector2.One"/>.
    /// </summary>
    public Vector2 Scale;

    /// <summary>
    ///     Gets or sets the origin of the sprite.
    /// </summary>
    public Vector2 Origin;

    /// <summary>
    ///     Gets or sets the effects of the sprite.
    /// </summary>
    public SpriteEffects Effects;

    /// <summary>
    ///     Gets or sets the source rectangle of the sprite.
    /// </summary>
    public Rectangle? SourceRectangle;

    /// <summary>
    ///     Gets or sets the destination rectangle of the sprite.
    /// </summary>
    public Rectangle? DestinationRectangle;

    /// <summary>
    ///     Gets or sets the rotation of the sprite, in radians.
    /// </summary>
    public float Rotation;

    /// <summary>
    ///     Gets or sets the color of the sprite. Defaults to <see cref="Color.White"/>.
    /// </summary>
    public Color Color;

    public Sprite() { }

    public void Draw(in SpriteBatchParameters parameters)
    {
        SpriteRendering.Draw(in this, in parameters);
    }
    
        /// <summary>
    ///     Sets the texture of the sprite.
    /// </summary>
    /// <param name="texture">The texture to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetTexture(Asset<Texture2D> texture)
    {
        Texture = texture;
        
        return this;
    }

    /// <summary>
    ///     Sets the position of the sprite.
    /// </summary>
    /// <param name="position">The position to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetPosition(Vector2 position)
    {
        Position = position;
        
        return this;
    }

    /// <summary>
    ///     Sets the scale of the sprite.
    /// </summary>
    /// <param name="scale">The scale to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetScale(Vector2 scale)
    {
        Scale = scale;
        
        return this;
    }

    /// <summary>
    ///     Sets the origin of the sprite.
    /// </summary>
    /// <param name="origin">The origin to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetOrigin(Vector2 origin)
    {
        Origin = origin;
        
        return this;
    }

    /// <summary>
    ///     Sets the effects of the sprite.
    /// </summary>
    /// <param name="effects">The effects to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetEffects(SpriteEffects effects)
    {
        Effects = effects;
        
        return this;
    }

    /// <summary>
    ///     Sets the source rectangle of the sprite.
    /// </summary>
    /// <param name="sourceRectangle">The source rectangle to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetSourceRectangle(Rectangle? sourceRectangle)
    {
        SourceRectangle = sourceRectangle;
        
        return this;
    }

    /// <summary>
    ///     Sets the destination rectangle of the sprite.
    /// </summary>
    /// <param name="destinationRectangle">The destination rectangle to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetDestinationRectangle(Rectangle? destinationRectangle)
    {
        DestinationRectangle = destinationRectangle;
        
        return this;
    }

    /// <summary>
    ///     Sets the rotation of the sprite.
    /// </summary>
    /// <param name="rotation">The rotation to set, in radians.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetRotation(float rotation)
    {
        Rotation = rotation;
        
        return this;
    }

    /// <summary>
    ///     Sets the color of the sprite.
    /// </summary>
    /// <param name="color">The color to set.</param>
    /// <returns>The updated <see cref="Sprite"/>.</returns>
    public Sprite SetColor(Color color)
    {
        Color = color;
        
        return this;
    }
}