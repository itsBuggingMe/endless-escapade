using EndlessEscapade.Utilities;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

/// <summary>
///     Represents a <see cref="GenCondition"/> which checks whether a <see cref="Tile"/> instance has a tile or not.
/// </summary>
public sealed class HasTile : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].HasTile;
    }
}