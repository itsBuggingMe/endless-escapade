using Terraria.GameContent.ObjectInteractions;
using Terraria.ObjectData;

namespace EndlessEscapade.Framework;

public abstract class ModDoorClosedTile<TModItem, TModTile> : ModTile
    where TModItem : ModItem
    where TModTile : ModTile
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        
        Main.tileSolid[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;
        
        TileID.Sets.DrawsWalls[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.NotReallySolid[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.OpenDoorID[Type] = ModContent.TileType<TModTile>();

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);

        AdjTiles = new int[]
        {
            TileID.ClosedDoor
        };

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.ClosedDoor, 0));
        TileObjectData.addTile(Type);
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void MouseOver(int i, int j)
    {
        base.MouseOver(i, j);

        var player = Main.LocalPlayer;

        player.noThrow = 2;

        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<TModItem>();
    }
}