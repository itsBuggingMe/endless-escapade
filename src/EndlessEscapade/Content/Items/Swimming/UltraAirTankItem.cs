namespace EndlessEscapade.Content.Items.Swimming;

public class UltraAirTankItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 24;
        Item.height = 40;
    }
}