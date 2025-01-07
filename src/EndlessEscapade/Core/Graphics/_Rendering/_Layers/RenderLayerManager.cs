using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EndlessEscapade.Utilities;
using ReLogic.Content;

namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayerManager : ModSystem
{
    private static readonly Dictionary<string, IRenderLayer> layersByName = [];
    private static readonly Dictionary<RenderLevel, RenderGroup> layersByLevel = [];
    
    public override void Load()
    {
        base.Load();
        
        Main.QueueMainThreadAction
        (
            () =>
            {
                Register(new BufferedRenderLayer(RenderLevel.Foreground, "Boids", 0f, Main.screenWidth, Main.screenHeight));
                Register(new BufferedRenderLayer(RenderLevel.Foreground, "Particles", 1f, Main.screenWidth, Main.screenHeight));
            }
        );
        
        On_Main.DrawProjectiles += Main_DrawProjectiles_Hook;
    }

    public override void PostUpdateEverything()
    {
        base.PostUpdateEverything();
        
        var asset = ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Items/Gliders/GliderItem", AssetRequestMode.ImmediateLoad);
     
        Get("Particles").Draw(new Sprite()
        {
            Texture = asset,
            Color = Color.Red,
            Rotation = Main.GameUpdateCount * 0.1f,
            Position = Main.MouseScreen,
            Origin = asset.Size() / 2f
        });
        
        Get("Boids").Draw(new Sprite()
        {
            Texture = asset,
            Color = Color.Green,
            Rotation = Main.GameUpdateCount * 0.01f,
            Position = Main.MouseScreen,
            Origin = asset.Size() / 2f
        });
    }

    public static void Register(IRenderLayer layer)
    {
        if (layersByName.ContainsKey(layer.Name))
        {
            throw new InvalidRenderLayerException($@"Layer ""{layer.Name}"" already exists.");
        }

        layersByName[layer.Name] = layer;

        if (!layersByLevel.ContainsKey(layer.Level))
        {
            layersByLevel[layer.Level] = new RenderGroup();
        }
        
        layersByLevel[layer.Level].Add(layer);
    }

    public static bool TryGet(string name, out IRenderLayer result)
    {
        return layersByName.TryGetValue(name, out result);
    }

    public static IRenderLayer Get(string name)
    {
        return layersByName[name]?? throw new InvalidRenderLayerException($@"Layer ""{name}"" does not exist.");
    }

    private static void RenderLayers(RenderLevel level)
    {
        if (!layersByLevel.TryGetValue(level, out var group))
        {
            return;
        }
        
        var graphicsDevice = Main.graphics.GraphicsDevice;
        
        var depthBuffer = new DepthStencilState
        {
            DepthBufferEnable = true,
            DepthBufferWriteEnable = true,
            DepthBufferFunction = CompareFunction.Less
        };
        
        graphicsDevice.DepthStencilState = depthBuffer;

        var spriteBatch = Main.spriteBatch;
        
        var oldParameters = spriteBatch.Capture();
        var currentParameters = spriteBatch.Capture();
        
        var isBatching = spriteBatch.beginCalled;

        if (isBatching)
        {
            spriteBatch.End();
        }
        
        spriteBatch.Begin(in oldParameters);

        foreach (var layer in group)
        {
            layer.Render();
            
            currentParameters = layer.Parameters;
        }
        
        spriteBatch.End();

        if (!isBatching)
        {
            return;
        }
        
        spriteBatch.Begin(in oldParameters);
    }
    
    private static void Main_DrawProjectiles_Hook(On_Main.orig_DrawProjectiles orig, Main self)
    {
        RenderLayers(RenderLevel.Foreground);
        
        orig(self);
    }
}