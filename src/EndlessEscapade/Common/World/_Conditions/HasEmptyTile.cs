using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class HasEmptyTile : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        var tile = Framing.GetTileSafely(x, y);

        return !tile.HasTile;
    }
}