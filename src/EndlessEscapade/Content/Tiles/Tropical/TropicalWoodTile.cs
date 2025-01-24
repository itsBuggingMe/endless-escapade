using EndlessEscapade.Common.Tiles;
using EndlessEscapade.Framework;

namespace EndlessEscapade.Content.Tiles.Tropical;

public class TropicalWoodTile : ModCompositeTile
{
    public override int HorizontalChunkCount { get; } = 3;

    public override int VerticalChunkCount { get; } = 1;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileMergeDirt[Type] = false;
        Main.tileSolid[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;

        AddMapEntry(new Color(102, 55, 45));

        HitSound = SoundID.Dig;
        DustType = DustID.WoodFurniture;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }
}