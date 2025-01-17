using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayer : IRenderLayer
{
    /// <summary>
    ///     Gets the name of the render layer.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets or sets the buffer of the render layer.
    /// </summary>
    public RenderTarget2D Buffer { get; private set; }

    /// <summary>
    ///     Gets or sets the callbacks of the render layer.
    /// </summary>
    public List<RenderCallback> Callbacks { get; private set; } = new();
    
    /// <summary>
    ///     Gets or sets the sprite batch parameters of the render layer.
    /// </summary>
    public SpriteBatchParameters Parameters { get; private set; } = new();

    public RenderLayer(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));
        
        Name = name;
        Buffer = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2, false, SurfaceFormat.Color, DepthFormat.Depth16);
        
        On_Main.CheckMonoliths += Main_CheckMonoliths_Hook;
    }

    public void Fill()
    {
        foreach (var callback in Callbacks)
        {
            var context = new RenderContext();
            
            callback?.Invoke(out context);

            Parameters = context.Parameters;
        }
    }

    public void Render()
    {
        var spriteBatch = Main.spriteBatch;
    }

    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            Buffer?.Dispose();
        }
        
        Buffer = null;
    }
    
    // Render layer contents are filled during Main::CheckMonoliths because it's the earliest time in which the screen position is updated.
    private void Main_CheckMonoliths_Hook(On_Main.orig_CheckMonoliths orig)
    {
        Fill();

        orig();
    }

    ~RenderLayer()
    {
        Dispose(false);
    }
}