namespace EndlessEscapade.Core.Graphics;

/// <summary>
///     Handles rendering the player's full sprite and combining it into a single texture, stored as a
///     <see cref="RenderTarget2D"/>.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class PlayerRendererSystem : ModSystem
{
    /// <summary>
    ///     The framebuffer that holds the player's full texture.
    /// </summary>
    public static RenderTarget2D Target { get; private set; }

    public override void Load() {
        base.Load();

        Main.QueueMainThreadAction(static () => Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight));

        Main.OnResolutionChanged += Main_OnResolutionChanged_Event;
    }

    public override void Unload() {
        base.Unload();

        Main.QueueMainThreadAction(
	        static () => {
		        Target?.Dispose();
		        Target = null;
	        }
	    );

        Main.OnResolutionChanged -= Main_OnResolutionChanged_Event;
    }

    public override void PreUpdateEntities() {
	    base.PreUpdateEntities();

	    var spriteBatch = Main.spriteBatch;
	    var device = Main.graphics.GraphicsDevice;

	    var oldTargets = device.GetRenderTargets();

	    device.SetRenderTarget(Target);
	    device.Clear(Color.Transparent);

	    spriteBatch.Begin(
		    default,
		    default,
		    Main.DefaultSamplerState,
		    default,
		    Main.Rasterizer,
		    default,
		    Main.GameViewMatrix.TransformationMatrix
		);

	    var player = Main.LocalPlayer;

	    if (player.active) {
		    Main.PlayerRenderer?.DrawPlayer(Main.Camera, player, player.position, player.fullRotation, player.fullRotationOrigin);
	    }

	    spriteBatch.End();

	    device.SetRenderTargets(oldTargets);
    }

    private static void Main_OnResolutionChanged_Event(Vector2 resolution) {
        Main.QueueMainThreadAction(
            () => {
	            Target?.Dispose();
                Target = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)resolution.X, (int)resolution.Y);
            }
        );
    }
}
