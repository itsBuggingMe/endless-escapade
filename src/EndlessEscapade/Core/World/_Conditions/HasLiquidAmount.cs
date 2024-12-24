using Terraria.WorldBuilding;

namespace EndlessEscapade.Core.World;

public sealed class HasLiquidAmount(byte amount) : GenCondition
{
    protected override bool CheckValidity(int x, int y)
    {
        var tile = Framing.GetTileSafely(x, y);

        return tile.LiquidAmount >= amount;
    }
}