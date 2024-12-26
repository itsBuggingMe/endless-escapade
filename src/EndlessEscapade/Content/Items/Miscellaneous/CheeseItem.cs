using EndlessEscapade.Utilities;

namespace EndlessEscapade.Content.Items.Miscellaneous;

public class CheeseItem : ModItem
{
    public const int BUFF_DURATION = 10 * 60 * 60;
    
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        
        Item.width = 50;
        Item.height = 34;
    }

    public override void OnConsumeItem(Player player)
    {
        base.OnConsumeItem(player);
        
        player.AddBuff(BuffID.WellFed, BUFF_DURATION);
    }
}