using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayer : IRenderLayer
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    private static SpriteBatch SpriteBatch => Main.spriteBatch;

    private List<RenderCallback> Callbacks { get; } = new();

    /// <summary>
    ///     Gets the sprite batch parameters of the render layer.
    /// </summary>
    public SpriteBatchParameters Parameters { get; } = new();

    /// <summary>
    ///     Gets the name of the render layer.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets or sets the buffer of the render layer.
    /// </summary>
    public RenderTarget2D Buffer { get; private set; }
    
    public RenderLayer(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name;
        Buffer = new RenderTarget2D(Device, Main.screenWidth / 2, Main.screenHeight / 2, false, SurfaceFormat.Color, DepthFormat.Depth16);
    }

    public void Load()
    {
        On_Main.CheckMonoliths += Main_CheckMonoliths_FillBuffer;

        Main.OnResolutionChanged += Main_ResolutionChanged_ResizeBuffer;
    }

    public void Unload()
    {
        Main.OnResolutionChanged -= Main_ResolutionChanged_ResizeBuffer;
    }

    public void Fill()
    {
        var bindings = Device.GetRenderTargets();

        Device.SetRenderTarget(Buffer);
        Device.Clear(Color.Transparent);

        SpriteBatch.Begin(Parameters);

        foreach (var callback in Callbacks)
        {
            callback?.Invoke(new RenderContext());
        }

        SpriteBatch.End();

        Device.SetRenderTargets(bindings);

        Callbacks.Clear();
    }

    public void Render()
    {
        var batchParameters = SpriteBatch.Capture();
        var batch = SpriteBatch.beginCalled;

        if (batch)
        {
            SpriteBatch.End();
        }

        SpriteBatch.Begin(Parameters);
        SpriteBatch.Draw(Buffer, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
        SpriteBatch.End();

        if (!batch)
        {
            return;
        }

        SpriteBatch.Begin(in batchParameters);
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
    private void Main_CheckMonoliths_FillBuffer(On_Main.orig_CheckMonoliths orig)
    {
        Fill();

        orig();
    }

    private void Main_ResolutionChanged_ResizeBuffer(Vector2 resolution)
    {
        Main.QueueMainThreadAction
        (
            () =>
            {
                Buffer?.Dispose();
                Buffer = new RenderTarget2D(Device, (int)(resolution.X / 2f), (int)(resolution.Y / 2f), false, SurfaceFormat.Color, DepthFormat.Depth16);
            }
        );
    }

    ~RenderLayer()
    {
        Dispose(false);
    }
}