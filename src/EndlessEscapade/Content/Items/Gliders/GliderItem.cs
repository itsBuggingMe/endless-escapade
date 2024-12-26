using EndlessEscapade.Common.World;
using Terraria.WorldBuilding;

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

    public override void HoldStyle(Player player, Rectangle heldItemFrame)
    {
        base.HoldStyle(player, heldItemFrame);
        
        player.itemLocation.X += -100f * player.direction;

        var landing = WorldUtils.Find
        (
            player.Center.ToTileCoordinates(),
            Searches.Chain
            (
                new Searches.Rectangle(1, 5),
                new Conditions.IsSolid()
            ),
            out _
        );

        if (!player.TryGetModPlayer(out GliderPlayer gliderPlayer) || landing)
        {
            return;
        }

        player.itemLocation.Y -= 10f;
        
        gliderPlayer.Enabled = true;
    }
}