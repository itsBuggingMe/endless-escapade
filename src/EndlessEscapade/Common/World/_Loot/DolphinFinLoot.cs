using EndlessEscapade.Content.Items.Oceanographer;
using EndlessEscapade.Core.World;

namespace EndlessEscapade.Common.World;

public sealed class DolphinFinLoot : ModChestLoot
{
    public override int ItemType { get; } = ModContent.ItemType<DolphinFinItem>();
    
    public override int ChanceDenominator { get; } = 3;
    
    public override int[] Frames { get; } = [17];
}