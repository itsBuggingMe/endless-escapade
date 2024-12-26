namespace EndlessEscapade.Content.Items.Swimming;

public class AirTankItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.width = 20;
        Item.height = 36;
    }
}