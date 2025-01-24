using EndlessEscapade.Utilities;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Framework.World;

/// <summary>
///     Provides a <see cref="GenAction"/> which extends a tile downwards until it reaches a solid tile.
/// </summary>
public sealed class ExtendDownwardsAction : GenAction
{
    public override bool Apply(Point origin, int x, int y, params object[] args)
    {
        if (!GenBase._tiles[x, y].HasTile)
        {
            return false;
        }

        var tile = GenBase._tiles[x, y];
        
        y++;

        while (WorldGen.InWorld(x, y) && !GenBase._tiles[x, y].IsSolid())
        {
            GenBase._tiles[x, y].CopyFrom(tile);
            GenBase._tiles[x, y].CopyPaintAndCoating(tile);

            WorldGen.SlopeTile(x, y);

            y++;
        }

        return UnitApply(origin, x, y, args);
    }
}