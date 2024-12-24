namespace EndlessEscapade.Content.Items.Gliders;

public class GliderItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.noWet = true;

        Item.width = 90;
        Item.height = 38;

        Item.holdStyle = ItemHoldStyleID.HoldFront;
    }
}