using EndlessEscapade.Content.Dusts;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class WaterRainSystem : ModSystem
{
    public override void Load()
    {
        base.Load();

        On_Rain.Update += Rain_Update_Hook;
    }

    private static void Rain_Update_Hook(On_Rain.orig_Update orig, Rain self)
    {
        orig(self);

        var isWet = Collision.WetCollision(self.position, 2, 2);

        if (!isWet || Main.rand.NextFloat(250f) <= Main.gfxQuality * 100f)
        {
            return;
        }

        self.active = false;

        var dust = Dust.NewDustDirect(self.position, 2, 2, ModContent.DustType<BubbleDust>());
        var tile = Framing.GetTileSafely(self.position.ToTileCoordinates());

        if (tile.LiquidAmount <= 0)
        {
            return;
        }

        dust.velocity = tile.LiquidType switch
        {
            LiquidID.Water => self.velocity / 4f,
            LiquidID.Lava => -self.velocity.SafeNormalize(Vector2.Zero),
            LiquidID.Honey => -self.velocity.SafeNormalize(Vector2.Zero) / 2f,
            LiquidID.Shimmer => new Vector2(self.velocity.X, -self.velocity.Y).SafeNormalize(Vector2.Zero) * 2f,
            _ => self.velocity / 4f
        };

        dust.color = tile.LiquidType switch
        {
            LiquidID.Lava => new Color(230, 174, 158),
            LiquidID.Honey => new Color(230, 227, 158),
            LiquidID.Shimmer => new Color(250, 212, 246),
            _ => dust.color
        };

        dust.scale += 0.5f * Main.cloudAlpha;
    }
}