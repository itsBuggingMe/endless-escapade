using EndlessEscapade.Content.Projectiles.Tennis;

namespace EndlessEscapade.Content.Items.Tennis;

public class TennisBallItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Item.noUseGraphic = true;
        Item.consumable = false;
        
        Item.width = 32;
        Item.height = 32;
        
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ModContent.ProjectileType<TennisBallProjectile>();
    }
}