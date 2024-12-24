namespace EndlessEscapade.Content.Items.Swimming;

public class RebreatherItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 32;
        Item.height = 26;
    }
}