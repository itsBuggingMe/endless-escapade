using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

/// <summary>
///     Represents a <see cref="GenCondition"/> which checks whether a <see cref="Tile"/> instance has water or not.
/// </summary>
public sealed class HasWater : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].LiquidAmount > 0 && GenBase._tiles[x, y].LiquidType == LiquidID.Water;
    }
}