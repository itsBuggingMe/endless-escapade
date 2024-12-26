using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

public sealed class HasWater : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        return GenBase._tiles[x, y].LiquidAmount > 0 && GenBase._tiles[x, y].LiquidType == LiquidID.Water;
    }
}