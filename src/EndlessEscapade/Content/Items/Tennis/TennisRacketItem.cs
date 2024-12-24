using EndlessEscapade.Content.Projectiles.Tennis;

namespace EndlessEscapade.Content.Items.Tennis;

public class TennisRacketItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Item.noUseGraphic = true;
        Item.channel = true;

        Item.width = 76;
        Item.height = 76;
        
        Item.damage = 20;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.shoot = ModContent.ProjectileType<TennisRacketProjectile>();
    }

    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] == 0;
    }
}