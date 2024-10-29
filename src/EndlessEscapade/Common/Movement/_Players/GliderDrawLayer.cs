using Terraria.DataStructures;
using Terraria.GameContent;

namespace EndlessEscapade.Common.Movement;

public sealed class GliderDrawLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() {
        return new AfterParent(PlayerDrawLayers.MountFront);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var drawPlayer = drawInfo.drawPlayer;

        return drawPlayer.TryGetModPlayer(out GliderPlayer gliderPlayer) && gliderPlayer.Enabled;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        var drawPlayer = drawInfo.drawPlayer;

        var item = drawPlayer.HeldItem;

        if (item.IsAir) {
            return;
        }

        Main.instance.LoadItem(item.type);

        var texture = TextureAssets.Item[item.type].Value;

        var drawPosition = drawPlayer.itemLocation - Main.screenPosition;

        var data = new DrawData(
            texture,
            drawPosition,
            null,
            Color.White,
            drawPlayer.itemRotation,
            texture.Size() / 2f,
            1f,
            drawPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None
        );

        drawInfo.DrawDataCache.Add(data);
    }
}
