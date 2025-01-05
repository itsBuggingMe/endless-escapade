using System.Collections.Generic;
using EndlessEscapade.Core.ECS;
using EndlessEscapade.Utilities;
using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class SpriteRendering : ModSystem
{
    public override void Load()
    {
        base.Load();
        
        On_Main.CheckMonoliths += Main_CheckMonoliths_Hook;
    }

    private static void Main_CheckMonoliths_Hook(On_Main.orig_CheckMonoliths orig)
    {
        orig();

        ref var container = ref ComponentManager.Get<SpriteContainer>();
        
        var spriteBatch = Main.spriteBatch;
        var graphicsDevice = Main.graphics.GraphicsDevice;

        var bindings = graphicsDevice.GetRenderTargets();

        var buffer = new DepthStencilState
        {
            DepthBufferEnable = true,
            DepthBufferFunction = CompareFunction.Less
        };

        graphicsDevice.DepthStencilState = buffer;

        var currentParameters = new SpriteBatchParameters();
        
        graphicsDevice.SetRenderTarget(Buffer);
        graphicsDevice.Clear(Color.Transparent);

        spriteBatch.Begin(in currentParameters);

        foreach (var entry in container.Entries)
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
                    entry.Layer.Id
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
                    entry.Layer.Id
                );
            }

            currentParameters = entry.Parameters;
        }
        
        spriteBatch.End();
        
        graphicsDevice.SetRenderTargets(bindings);
        
        container.Entries.Clear();
    }
}