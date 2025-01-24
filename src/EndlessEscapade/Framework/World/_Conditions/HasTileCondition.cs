using Terraria.WorldBuilding;

namespace EndlessEscapade.Framework.World;

/// <summary>
///     Provides a <see cref="GenCondition"/> which checks whether a tile has a tile or not.
/// </summary>
public sealed class HasTileCondition : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].HasTile;
    }
}