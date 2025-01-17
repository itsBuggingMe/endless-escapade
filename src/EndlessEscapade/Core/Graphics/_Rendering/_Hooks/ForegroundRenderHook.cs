using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public sealed class ForegroundRenderHook : RenderHook
{
    public override void Load(Mod mod)
    {
        On_Main.DrawProjectiles += Main_DrawProjectiles_Hook;
    }

    public override void Unload() { }
    
    private void Main_DrawProjectiles_Hook(On_Main.orig_DrawProjectiles orig, Main self)
    {
        var graphicsDevice = Main.graphics.GraphicsDevice;
        
        var depthStencilState = new DepthStencilState
        {
            DepthBufferEnable = true,
            DepthBufferWriteEnable = true,
            DepthBufferFunction = CompareFunction.Less
        };

        var oldDepthStencilState = graphicsDevice.DepthStencilState;
        
        graphicsDevice.DepthStencilState = depthStencilState;
        
        // TODO: Draw layers.

        graphicsDevice.DepthStencilState = oldDepthStencilState;

        orig(self);
    }
}