using EndlessEscapade.Common.Projectiles;
using Terraria.Audio;

namespace EndlessEscapade.Content.Projectiles.Hatchets;

public class CopperHatchetProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Projectile.usesLocalNPCImmunity = true;
        Projectile.friendly = true;

        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.penetrate = -1;

        Projectile.localNPCHitCooldown = 30;
    }

    public override void AI()
    {
        base.AI();

        Projectile.velocity.Y += 0.2f;
        
        Projectile.rotation += Projectile.velocity.X * 0.05f;
    }
}