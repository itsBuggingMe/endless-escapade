using Terraria.Audio;
using Terraria.GameContent;

namespace EndlessEscapade.Content.Projectiles.Tennis;

public class TennisRacketProjectile : ModProjectile
{
    public const float COOLDOWN = 30f;

    public const float RADIAL = 75f;

    public const float DAMPENING = 0.1f;

    public const float INVERSE_SPEED = 100f;

    public static readonly SoundStyle BallHitSound = new SoundStyle($"{nameof(EndlessEscapade)}/Assets/Sounds/Custom/TennisBall")
    {
        PitchVariance = 0.25f
    };
    
    private Player Player => Main.player[Projectile.owner];

    private ref float Timer => ref Projectile.ai[0];
    
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.projFrames[Type] = 8;
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        
        Projectile.width = 114;
        Projectile.height = 50;
        
        Projectile.penetrate = -1;
    }

    public override void AI()
    {
        base.AI();

        var player = Main.player[Projectile.owner];

        if (!player.active || player.dead || player.ghost || !player.channel)
        {
            Projectile.Kill();
            return;
        }
        
        UpdateCollision();
        UpdateFrame();
        UpdateMovement();
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var texture = TextureAssets.Projectile[Type].Value;

        var position = Projectile.Center - Main.screenPosition + new Vector2(DrawOffsetX, Projectile.gfxOffY);
        
        var frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
        var origin = frame.Size() / 2f + new Vector2(DrawOriginOffsetX, DrawOriginOffsetY);
        
        var effects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        Main.EntitySpriteDraw
        (
            texture, 
            Projectile.Center - Main.screenPosition,
            frame, 
            lightColor * (1 - (Projectile.alpha / 255f)),
            Projectile.rotation, 
            origin, 
            Projectile.scale,
            effects, 
            0
        );
        
        return false;
    }

    private void UpdateCollision()
    {
        if (Timer > 0f)
        {
            Timer--;
        }
        else
        {
            foreach (var projectile in Main.ActiveProjectiles)
            {
                var hitbox = new Rectangle((int)Projectile.position.X + 14, (int)Projectile.position.Y + 2, Projectile.width - 14, Projectile.height - 2);

                if (!projectile.Hitbox.Intersects(hitbox) || projectile.type != ModContent.ProjectileType<TennisBallProjectile>())
                {
                    continue;
                }

                projectile.velocity = new Vector2(Projectile.velocity.X * 1.5f, Projectile.velocity.Y);
                projectile.netUpdate = true;

                Timer = COOLDOWN;

                Projectile.netUpdate = true;

                SoundEngine.PlaySound(in BallHitSound, Projectile.Center);
            }
        }
    }

    private void UpdateFrame()
    {
        var start = Player.direction == -1 ? Player.Center.X : Projectile.Center.X;
        var end = Player.direction == -1 ? Projectile.Center.X : Player.Center.X;

        var frameCount = Main.projFrames[Type];
        var frame = (int)((start - end) / RADIAL * (frameCount / 2f) + (frameCount / 2f));

        Projectile.frame = frame;
        
        if (Player.direction == -1)
        {
            Projectile.rotation = MathHelper.Pi;
        }
        else
        { 
            Projectile.rotation = 0f;
        }
        
        Projectile.frame = (int)MathHelper.Clamp(Projectile.frame, 0, frameCount - 1);
    }

    private void UpdateMovement()
    {
        var offset = new Vector2(RADIAL);
        var direction = Vector2.Clamp(Main.MouseWorld, Player.Center - offset, Player.Center + offset);

        Projectile.velocity += (direction - Projectile.Center) / INVERSE_SPEED - (Projectile.velocity * DAMPENING);
    }
}