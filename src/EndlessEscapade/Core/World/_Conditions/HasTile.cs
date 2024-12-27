using EndlessEscapade.Utilities;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

public sealed class HasTile : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].HasTile;
    }
}