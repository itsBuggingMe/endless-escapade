namespace EndlessEscapade.Content.Items.Swimming;

public class LargeAirTankItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 22;
        Item.height = 38;
    }
}