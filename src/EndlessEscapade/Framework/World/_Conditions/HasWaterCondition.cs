using Terraria.WorldBuilding;

namespace EndlessEscapade.Framework.World;

/// <summary>
///     Provides a <see cref="GenCondition"/> which checks whether a tile has water or not.
/// </summary>
public sealed class HasWaterCondition : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].LiquidAmount > 0 && GenBase._tiles[x, y].LiquidType == LiquidID.Water;
    }
}