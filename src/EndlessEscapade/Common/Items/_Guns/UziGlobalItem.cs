using EndlessEscapade.Content.Gores;
using EndlessEscapade.Framework.Core;

namespace EndlessEscapade.Common.Items.Guns;

public sealed class UziGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.Uzi;
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