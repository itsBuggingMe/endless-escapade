using EndlessEscapade.Content.Gores;
using EndlessEscapade.Core.Items;

namespace EndlessEscapade.Common.Items.Guns;

public sealed class TacticalShotgunGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.TacticalShotgun;
    }

    public override void SetDefaults(Item entity)
    {
        if (!entity.TryEnable(out ItemBulletCasingsComponent? component))
        {
            return;
        }

        component.CasingType = ModContent.GoreType<ShellCasingGore>();
        component.CasingAmount = 6;
    }
}