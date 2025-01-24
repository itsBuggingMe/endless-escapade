using EndlessEscapade.Content.Gores;
using EndlessEscapade.Framework.Core;

namespace EndlessEscapade.Common.Items.Guns;

public sealed class RedRyderGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.RedRyder;
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