using Terraria.ObjectData;

namespace EndlessEscapade.Framework;

public abstract class ModPlatformTile : ModTile
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileSolid[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileID.Sets.Platforms[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);

        AdjTiles = new int[]
        {
            TileID.Platforms
        };

        TileObjectData.newTile.UsesCustomCanPlace = false;
        TileObjectData.newTile.LavaDeath = true;

        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16
        };

        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;

        TileObjectData.newTile.StyleHorizontal = true;

        TileObjectData.newTile.StyleMultiplier = 27;
        TileObjectData.newTile.StyleWrapLimit = 27;

        TileObjectData.addTile(Type);
    }

    public override void PostSetDefaults()
    {
        base.PostSetDefaults();
        
        Main.tileNoSunLight[Type] = false;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);
        
        num = fail ? 1 : 3;
    }
}