using EndlessEscapade.Utilities;

namespace EndlessEscapade.Common.Projectiles;

/// <summary>
///     <para>
///         Handles the transformation behavior of wooden arrows when they are submerged in specific liquids.
///     </para>
///     <para>
///         - When submerged in <see cref="LiquidID.Lava"/>, wooden arrows will transform into
///         <see cref="ProjectileID.FireArrow"/>.
///         <br />
///         - When submerged in <see cref="LiquidID.Shimmer"/>, wooden arrows will transform into
///         <see cref="ProjectileID.ShimmerArrow"/>.
///     </para>
/// </summary>
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
