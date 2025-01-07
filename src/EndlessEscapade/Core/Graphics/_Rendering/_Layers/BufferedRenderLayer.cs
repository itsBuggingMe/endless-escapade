using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public sealed class BufferedRenderLayer : IRenderLayer, IDisposable
{
    public SpriteBatchParameters Parameters { get; set; }

    public List<Sprite> Entries { get; } = [];

    public float Depth { get; }
    
    public string Name { get; }
    
    public RenderLevel Level { get; }
    
    public bool IsDisposed { get; private set; }
    
    public RenderTarget2D Buffer { get; private set; }

    internal BufferedRenderLayer(RenderLevel level, string name, float depth, int width, int height)
    {
        Level = level;
        
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name;
        
        ArgumentOutOfRangeException.ThrowIfNegative(depth, nameof(depth));

        Depth = depth;
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width, nameof(width));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height, nameof(height));

        Buffer = new RenderTarget2D(Main.graphics.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.Depth16);
        
        On_Main.CheckMonoliths += Main_CheckMonoliths_Hook;
    }

    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        
        if (disposing)
        {
            Buffer?.Dispose();
        }

        Buffer = null;
        
        IsDisposed = true;
    }
    
    public void Draw(in Sprite sprite)
    {
        Entries.Add(sprite);
    }

    public void Render()
    {
        var spriteBatch = Main.spriteBatch;

        spriteBatch.Draw
        (
            Buffer, 
            new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), 
            null,
            Color.White,
            0f,
            default,
            SpriteEffects.None,
            Depth
        );
    }

    private void Fill()
    {
        var spriteBatch = Main.spriteBatch;
        var graphicsDevice = Main.graphics.GraphicsDevice;

        var oldBindings = graphicsDevice.GetRenderTargets();
        
        graphicsDevice.SetRenderTarget(Buffer);
        graphicsDevice.Clear(Color.Transparent);
        
        spriteBatch.Begin(Parameters);
        
        var currentParameters = Parameters;

        foreach (var entry in Entries)
        {
            var needsBatchRestart = entry.Parameters.HasValue && currentParameters != entry.Parameters.Value;
            
            if (needsBatchRestart)
            {
                spriteBatch.End();
                spriteBatch.Begin(entry.Parameters.Value);
            }
            
            if (entry.DestinationRectangle.HasValue)
            {
                spriteBatch.Draw
                (
                    entry.Texture.Value,
                    entry.DestinationRectangle.Value,
                    entry.SourceRectangle,
                    entry.Color,
                    entry.Rotation,
                    entry.Origin,
                    entry.Effects,
                    Depth
                );
            }
            else
            {
                spriteBatch.Draw
                (
                    entry.Texture.Value,
                    entry.Position,
                    entry.SourceRectangle,
                    entry.Color,
                    entry.Rotation,
                    entry.Origin,
                    entry.Scale,
                    entry.Effects,
                    Depth
                );
            }
        }
        
        spriteBatch.End();
        
        graphicsDevice.SetRenderTargets(oldBindings);
        
        Entries.Clear();
    }
    
    private void Main_CheckMonoliths_Hook(On_Main.orig_CheckMonoliths orig)
    {
        orig();
        
        Fill();
    }
}