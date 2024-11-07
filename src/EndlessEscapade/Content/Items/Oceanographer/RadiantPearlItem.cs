namespace EndlessEscapade.Content.Items.Oceanographer;

public class RadiantPearlItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 28;
        Item.height = 28;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {
        base.UpdateAccessory(player, hideVisual);

        Main.instance.SpelunkerProjectileHelper.AddSpotToCheck(player.Center);
    }
}
