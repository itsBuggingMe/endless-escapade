using EndlessEscapade.Utilities.Extensions;

namespace EndlessEscapade.Common.Projectiles;

public sealed class WoodenArrowGlobalProjectile : GlobalProjectile
{
    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) {
        return entity.type == ProjectileID.WoodenArrowFriendly || entity.type == ProjectileID.WoodenArrowHostile;
    }

    public override void AI(Projectile projectile) {
        base.AI(projectile);

        if (projectile.lavaWet) {
            projectile.Transform(ProjectileID.FireArrow);
        }

        if (projectile.shimmerWet) {
            projectile.Transform(ProjectileID.ShimmerArrow);
        }
    }
}
