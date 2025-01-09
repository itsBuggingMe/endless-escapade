using EndlessEscapade.Content.Gores;
using EndlessEscapade.Core.Items;

namespace EndlessEscapade.Common.Items.Guns;

public sealed class VenusMagnumGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.VenusMagnum;
    }

    public override void SetDefaults(Item entity)
    {
        if (!entity.TryEnable(out ItemBulletCasingsComponent? component))
        {
            return;
        }

        component.CasingType = ModContent.GoreType<BulletCasingGore>();
        component.CasingAmount = 1;
    }
}