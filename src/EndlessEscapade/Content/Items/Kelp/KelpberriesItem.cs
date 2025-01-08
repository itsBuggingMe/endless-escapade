using Terraria.DataStructures;

namespace EndlessEscapade.Content.Items.Kelp;

public class KelpberriesItem : ModItem
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        
        ItemID.Sets.IsFood[Type] = true;
        
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        
        Item.DefaultToFood(22, 26, BuffID.WellFed, 10 * 60 * 60);
    }
}