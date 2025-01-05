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
        
        ComponentManager.Set(new SpriteContainer());

        RenderLayerManager.Register(RenderLevel.Foreground, "Boids", true);
        RenderLayerManager.Register(RenderLevel.Foreground, "Particles", true);
        
        On_Main.CheckMonoliths += Main_CheckMonoliths_Hook;
        On_Main.DrawProjectiles += Main_DrawProjectiles_Hook;
    }

    public override void PreUpdateEntities()
    {
        base.PreUpdateEntities();

        ref var container = ref ComponentManager.Get<SpriteContainer>();

        var asset = ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Items/Gliders/GliderItem", AssetRequestMode.ImmediateLoad);

        container.Entries.Add(new Sprite()
        {
            Texture = asset,
            Color = Color.Red,
            Rotation = Main.GameUpdateCount * 0.1f,
            Layer = RenderLayerManager.Get("Boids"),
            Position = Main.MouseScreen - asset.Size(),
            Parameters = new SpriteBatchParameters() { TransformMatrix = Matrix.CreateScale(0.5f, 0.5f, 1f) },
            Origin = asset.Size() / 2f
        });
        
        container.Entries.Add(new Sprite()
        {
            Texture = ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Items/Gliders/GliderItem", AssetRequestMode.ImmediateLoad),
            Color = Color.Green,
            Rotation = Main.GameUpdateCount * 0.01f,
            Layer = RenderLayerManager.Get("Particles"),
            Position = Main.MouseScreen - asset.Size(),
            Parameters = new SpriteBatchParameters() { TransformMatrix = Matrix.CreateScale(0.5f, 0.5f, 1f) },
            Origin = asset.Size() / 2f
        });
    }

    private static void Main_DrawProjectiles_Hook(On_Main.orig_DrawProjectiles orig, Main self)
    {
        var spriteBatch = Main.spriteBatch;
        var graphicsDevice = Main.graphics.GraphicsDevice;
        
        var depthBuffer = new DepthStencilState
        {
            DepthBufferEnable = true,
            DepthBufferWriteEnable = true,
            DepthBufferFunction = CompareFunction.Less
        };
        
        graphicsDevice.DepthStencilState = depthBuffer;
        
        spriteBatch.Begin
        (
            SpriteSortMode.FrontToBack,
            BlendState.AlphaBlend,
            Main.DefaultSamplerState,
            default,
            Main.Rasterizer,
            default,
            Main.GameViewMatrix.TransformationMatrix
        );

        foreach (var layer in RenderLayerManager.Enumerate())
        {
            if (layer.Level != RenderLevel.Foreground)
            {
                continue;
            }
            
            var depth = (RenderLayerManager.Count - layer.Id) / (float)RenderLayerManager.Count;
            
            spriteBatch.Draw
            (
                layer.Buffer, 
                new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                null,
                Color.White,
                0f,
                default,
                SpriteEffects.None,
                depth
            );
        }
        
        spriteBatch.End();
        
        orig(self);
    }

    private static void Main_CheckMonoliths_Hook(On_Main.orig_CheckMonoliths orig)
    {
        orig();

        ref var container = ref ComponentManager.Get<SpriteContainer>();
        
        var spriteBatch = Main.spriteBatch;
        var graphicsDevice = Main.graphics.GraphicsDevice;

        var bindings = graphicsDevice.GetRenderTargets();

        var currentParameters = new SpriteBatchParameters();
        
        spriteBatch.Begin(in currentParameters);

        foreach (var entry in container.Entries)
        {
            var needsBufferRestart = true;

            if (needsBufferRestart)
            {
                graphicsDevice.SetRenderTarget(entry.Layer.Buffer);
                graphicsDevice.Clear(Color.Transparent);
            }
            
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
        
        container.Entries.Clear();
    }
}