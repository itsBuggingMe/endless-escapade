using EndlessEscapade.Content.Tiles.Kelp;
using EndlessEscapade.Content.Tiles.Shallows;
using EndlessEscapade.Content.Tiles.Trenches;
using EndlessEscapade.Content.Tiles.Twilight;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;

namespace EndlessEscapade.Content.Tiles.Ocean;

public class PinkSeaweedTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileID.Sets.SwaysInWindBasic[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 0, 0);

        TileObjectData.newTile.WaterPlacement = LiquidPlacement.OnlyInLiquid;
        TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
        
        TileObjectData.newTile.LavaDeath = true;
        
        TileObjectData.newTile.AnchorValidTiles = new[]
        {
            TileID.Sand,
            ModContent.TileType<GemsandTile>(),
            ModContent.TileType<LightGemsandTile>(),
            ModContent.TileType<DarkGemsandTile>(),
            ModContent.TileType<LightGemsandstoneTile>(),
            ModContent.TileType<GemsandstoneTile>(),
            ModContent.TileType<DarkGemsandstoneTile>(),
            ModContent.TileType<KelpLeafTile>(),
            ModContent.TileType<PinkSeaweedTile>(),
            ModContent.TileType<KelpMossTile>()
        };
        
        TileObjectData.newTile.AnchorTop = default;

        TileObjectData.addTile(Type);

        MineResist = 1f;

        AnimationFrameHeight = 18;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }
    
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        base.AnimateTile(ref frame, ref frameCounter);
        
        frameCounter++;
        
        if (frameCounter < 5)
        {
            return;
        }

        frame++;
        
        if (frame >= 8)
        {
            frame = 0;
        }

        frameCounter = 0;
    }
}