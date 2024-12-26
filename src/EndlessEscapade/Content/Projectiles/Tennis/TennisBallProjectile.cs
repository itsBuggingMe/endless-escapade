namespace EndlessEscapade.Content.Projectiles.Tennis;

public class TennisBallProjectile : ModProjectile
{
    public const float BOUNCE = 0.7f;

    public const float GRAVITY = 0.3f;

    public const float MAX_GRAVITY = 10f;
    
    public const float DEACCELERATION = 0.98f;
    
    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Projectile.tileCollide = true;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        
        Projectile.width = 18;
        Projectile.height = 18;
        
        Projectile.penetrate = -1;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X * BOUNCE;
        }

        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y * BOUNCE;
        }
        
        return false;
    }

    public override void AI()
    {
        base.AI();
        
        UpdateGravity();
        
        Projectile.velocity.X *= DEACCELERATION;

        Projectile.rotation += Projectile.velocity.X / 16f;
    }

    private void UpdateGravity()
    {
        Projectile.velocity.Y += GRAVITY;
        
        if (Projectile.velocity.Y <= MAX_GRAVITY)
        {
            return;
        }
        
        Projectile.velocity.Y = MAX_GRAVITY;
    }
}