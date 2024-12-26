namespace EndlessEscapade.Content.Items.Miscellaneous;

public class CheeseItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;
        
        Item.width = 50;
        Item.height = 34;
    }

    public override bool ConsumeItem(Player player)
    {
        player.AddBuff(BuffID.WellFed, 10 * 60 * 60);
        
        return base.ConsumeItem(player);
    }
}