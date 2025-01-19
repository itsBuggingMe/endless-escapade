using System.Collections.Generic;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public abstract class RenderHook : ILoadable
{
    /// <summary>
    ///     Gets the game's <see cref="GraphicsDevice" /> instance. Shorthand for <c>Main.graphics.GraphicsDevice</c>.
    /// </summary>
    protected static GraphicsDevice GraphicsDevice => Main.graphics.GraphicsDevice;

    /// <summary>
    ///     Gets the game's <see cref="SpriteBatch" /> instance. Shorthand for <c>Main.spriteBatch</c>.
    /// </summary>
    protected static SpriteBatch SpriteBatch => Main.spriteBatch;

    /// <summary>
    ///     Gets the list of render entries for the render hook.
    /// </summary>
    protected List<RenderHookEntry> Entries { get; } = new();

    /// <summary>
    ///     Gets or sets the frame buffer used by the render hook.
    /// </summary>
    public RenderTarget2D Buffer { get; private set; }

    /// <summary>
    ///     Gets the sprite batch parameters used by the frame buffer.
    /// </summary>
    public virtual SpriteBatchParameters BufferParameters => new
    (
        SpriteSortMode.Deferred,
        BlendState.NonPremultiplied, 
        SamplerState.PointClamp,
        default,
        Main.Rasterizer,
        default, 
        Main.GameViewMatrix.TransformationMatrix
    );
    
    /// <summary>
    ///     Gets the sprite batch parameters used by the render elements.
    /// </summary>
    public virtual SpriteBatchParameters ElementParameters => new
    (
        SpriteSortMode.Deferred,
        BlendState.NonPremultiplied, 
        SamplerState.PointClamp,
        default,
        Main.Rasterizer,
        default, 
        Matrix.CreateScale(0.5f, 0.5f, 1f)
    );

    public virtual void Load(Mod mod)
    {
        Main.QueueMainThreadAction(() => Buffer = new RenderTarget2D(GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2));

        On_Main.CheckMonoliths += Main_CheckMonoliths_Fill;

        Main.OnResolutionChanged += Main_OnResolutionChanged_Resize;
    }

    public virtual void Unload()
    {
        Main.OnResolutionChanged -= Main_OnResolutionChanged_Resize;

        Main.QueueMainThreadAction
        (
            () =>
            {
                Buffer?.Dispose();
                Buffer = null;
            }
        );
    }

    /// <summary>
    ///     Queues a render entry to be drawn to the frame buffer.
    /// </summary>
    /// <param name="entry">The render entry to queue.</param>
    public virtual void Queue(in RenderHookEntry entry)
    {
        Entries.Add(entry);
        Entries.Sort(static (left, right) => left.Parameters.CompareTo(right.Parameters));
    }

    /// <summary>
    ///     Draws the elements of the render hook to the frame buffer.
    /// </summary>
    protected virtual void Fill()
    {
        var bindings = GraphicsDevice.GetRenderTargets();

        GraphicsDevice.SetRenderTarget(Buffer);
        GraphicsDevice.Clear(Color.Transparent);

        SpriteBatch.Begin(ElementParameters);

        var currentParameters = ElementParameters;
        
        foreach (var entry in Entries)
        {
            var entryParameters = entry.Parameters;

            if (currentParameters != entryParameters)
            {
                SpriteBatch.End();
                SpriteBatch.Begin(in entryParameters);
            }
            
            entry.Draw(in entryParameters);
        }

        SpriteBatch.End();

        GraphicsDevice.SetRenderTargets(bindings);

        Entries.Clear();
    }

    /// <summary>
    ///     Draws the contents of the frame buffer to the screen.
    /// </summary>
    protected virtual void Draw()
    {
        SpriteBatch.Begin(BufferParameters);
        SpriteBatch.Draw(Buffer, RenderUtils.ScreenBounds, Color.White);
        SpriteBatch.End();
    }

    // Render hook contents are filled during Main::CheckMonoliths because it's the earliest time in which screen position is updated.
    private void Main_CheckMonoliths_Fill(On_Main.orig_CheckMonoliths orig)
    {
        Fill();

        orig();
    }

    private void Main_OnResolutionChanged_Resize(Vector2 resolution)
    {
        Main.QueueMainThreadAction
        (
            () =>
            {
                Buffer?.Dispose();
                Buffer = new RenderTarget2D(GraphicsDevice, (int)(resolution.X / 2f), (int)(resolution.Y / 2f));
            }
        );
    }
}