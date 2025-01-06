using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public sealed class Test : ModSystem
{
    public override void Load()
    {
        base.Load();
        
        
    }

    public override void PostUpdateDusts()
    {
        base.PostUpdateDusts();
        
        SpriteRendering.Draw<BackgroundLayer>(ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Items/GliderItem"), Main.MouseScreen, Color.White);
    }
}

public abstract class RenderLayer : IDisposable
{
    public SpriteBatchParameters Parameters { get; set; }
    
    public RenderTarget2D Buffer { get; private set; }

    internal List<Sprite> Entries { get; private set; } = [];

    public RenderLayer()
    {
        Main.QueueMainThreadAction(() => Buffer = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight));
    }
    
    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }
    
    internal virtual void Subscribe() 
    {
        Main.OnResolutionChanged += Resize;
    }

    internal virtual void Unsubscribe()
    {
        Main.OnResolutionChanged -= Resize;
    }
    
    protected virtual void Fill()
    {
        var spriteBatch = Main.spriteBatch;
        var graphicsDevice = Main.graphics.GraphicsDevice;

        var bindings = graphicsDevice.GetRenderTargets();

        graphicsDevice.SetRenderTarget(Buffer);
        graphicsDevice.Clear(Color.Transparent);
        
        var currentParameters = Parameters;
        
        spriteBatch.Begin(in currentParameters);

        foreach (var entry in Entries)
        {
            var needsBatchRestart = currentParameters != entry.Parameters;
            
            if (needsBatchRestart)
            {
                spriteBatch.End();
                spriteBatch.Begin(entry.Parameters);
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
                    0f
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
                    0f
                );
            }

            currentParameters = entry.Parameters;
        }
        
        spriteBatch.End();
        
        graphicsDevice.SetRenderTargets(bindings);
        
        Entries.Clear();
    }

    protected virtual void Render()
    {
        var spriteBatch = Main.spriteBatch;
        var snapshot = spriteBatch.Capture();

        var batching = spriteBatch.beginCalled;

        if (batching)
        {
            spriteBatch.End();
        }
        
        spriteBatch.Begin(Parameters);
        spriteBatch.Draw(Buffer, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
        spriteBatch.End();

        if (!batching)
        {
            return;
        }
        
        spriteBatch.Begin(in snapshot);
    }
    
    protected virtual void Resize(Vector2 resolution)
    {
        Main.QueueMainThreadAction
        (
            () =>
            {
                Buffer?.Dispose();
                Buffer = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)(resolution.X), (int)(resolution.Y));
            }
        );
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Buffer?.Dispose();
        }
        
        Entries?.Clear();
        Entries = null;
        
        Buffer = null;
    }
}