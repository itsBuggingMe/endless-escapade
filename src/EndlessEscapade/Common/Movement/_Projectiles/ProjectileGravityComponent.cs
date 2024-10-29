using EndlessEscapade.Core.EC;

namespace EndlessEscapade.Common.Movement;

/// <summary>
///     Represents a projectile component 
/// </summary>
public sealed class ProjectileGravityComponent : ProjectileComponent
{
    public struct GravityData(float velocity, float maxVelocity)
    {
        public readonly float Velocity = velocity;

        public readonly float MaxVelocity = maxVelocity;
    }

    public GravityData Data;

    public override void AI(Projectile projectile) {
        base.AI(projectile);

        projectile.velocity.Y += Data.Velocity;

        if (projectile.velocity.Y >= Data.MaxVelocity) {
            projectile.velocity.Y = Data.MaxVelocity;
        }
    }
}
