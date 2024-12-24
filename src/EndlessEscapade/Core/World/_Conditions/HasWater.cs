using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

public sealed class HasWater : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        var tile = Framing.GetTileSafely(x, y);

        return tile.LiquidType == LiquidID.Water;
    }
}