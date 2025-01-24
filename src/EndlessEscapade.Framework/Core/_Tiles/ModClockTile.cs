using Terraria.ObjectData;

namespace EndlessEscapade.Framework.Core;

public abstract class ModClockTile<TModItem> : ModTile where TModItem : ModItem
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileID.Sets.Clock[Type] = true;

        AdjTiles = new int[]
        {
            TileID.GrandfatherClocks
        };

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);

        TileObjectData.newTile.Height = 5;

        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            16,
            16,
            16,
            16
        };

        TileObjectData.addTile(Type);
    }
    
    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);
        
        num = fail ? 1 : 3;
    }

    // TODO: Make time calculation an utility method.
    public override bool RightClick(int x, int y)
    {
        var text = "AM";
        var time = Main.time;
        
        if (!Main.dayTime)
        {
            time += 54000.0;
        }

        time = time / 86400.0 * 24.0;
        time = time - 7.5 - 12.0;
        
        if (time < 0.0)
        {
            time += 24.0;
        }

        if (time >= 12.0)
        {
            text = "PM";
        }

        var intTime = (int)time;
        var deltaTime = time - intTime;
        
        deltaTime = (int)(deltaTime * 60.0);
        
        var text2 = string.Concat(deltaTime);
        
        if (deltaTime < 10.0)
        {
            text2 = "0" + text2;
        }

        if (intTime > 12)
        {
            intTime -= 12;
        }

        if (intTime == 0)
        {
            intTime = 12;
        }

        Main.NewText($"Time: {intTime}:{text2} {text}", 255, 240, 20);

        return true;
    }
}