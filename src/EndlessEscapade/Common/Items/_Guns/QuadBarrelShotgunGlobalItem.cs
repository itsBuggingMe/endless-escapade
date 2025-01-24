using EndlessEscapade.Content.Gores;
using EndlessEscapade.Framework;

namespace EndlessEscapade.Common.Items.Guns;

public sealed class QuadBarrelShotgunGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.QuadBarrelShotgun;
    }

    public override void SetDefaults(Item entity)
    {
        if (!entity.TryEnable(out ItemBulletCasingsComponent? component))
        {
            return;
        }

        component.CasingType = ModContent.GoreType<ShellCasingGore>();
        component.CasingAmount = 4;
    }
}