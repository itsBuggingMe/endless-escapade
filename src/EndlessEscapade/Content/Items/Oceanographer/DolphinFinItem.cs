namespace EndlessEscapade.Content.Items.Oceanographer;

public class DolphinFinItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 24;
        Item.height = 20;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);

        player.ignoreWater = true;
    }
}