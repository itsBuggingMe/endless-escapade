using EndlessEscapade.Core.EC;

namespace EndlessEscapade.Common.Movement;

/// <summary>
///     Represents a component responsible for applying gravity to projectiles.
/// </summary>
public sealed class ProjectileGravityComponent : ProjectileComponent
{
    public struct GravityData(float step, float max)
    {
        public float Step = step;

        public float Max = max;
    }

    public GravityData Data;

    public override void SafeAI(Projectile projectile) {
        base.SafeAI(projectile);

        projectile.velocity.Y += Data.Step;

        if (projectile.velocity.Y >= Data.Max) {
            projectile.velocity.Y = Data.Max;
        }
    }
}
