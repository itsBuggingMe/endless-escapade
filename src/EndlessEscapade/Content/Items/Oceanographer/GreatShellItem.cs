namespace EndlessEscapade.Content.Items.Oceanographer;

public class GreatShellItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 20;
        Item.height = 22;

        Item.defense = 3;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);

        // TODO: Find a way to decrease knockback instead of completely negating it.
        player.noKnockback = true;
    }
}