namespace EndlessEscapade.Content.Items.Lythen;

[AutoloadEquip(EquipType.Head)]
public class StormKnightCrestItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.width = 26;
        Item.height = 28;
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<LythenBarItem>(3)
            .AddTile(TileID.Anvils)
            .Register();
    }
    
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<StormKnightBreastplateItem>() && legs.type == ModContent.ItemType<StormKnightLeggingsItem>();
    }
}

[AutoloadEquip(EquipType.Body)]
public class StormKnightBreastplateItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.width = 34;
        Item.height = 20;
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<LythenBarItem>(5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}

[AutoloadEquip(EquipType.Legs)]
public class StormKnightLeggingsItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 14;
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient<LythenBarItem>(4)
            .AddTile(TileID.Anvils)
            .Register();
    }
}