using EndlessEscapade.Core.Graphics;
using Terraria.DataStructures;

namespace EndlessEscapade.Common.Movement;

/// <summary>
///     Handles the player's movement and visual effects while using a glider.
/// </summary>
public sealed class GliderPlayer : ModPlayer
{
    /// <summary>
    ///     Whether the player is currently using a glider or not.
    /// </summary>
    public bool Enabled { get; set; }

    public bool Takeoff { get; private set; }

    public override void ResetEffects() {
        base.ResetEffects();

        Enabled = false;
    }

    public override void PreUpdateMovement() {
        base.PreUpdateMovement();

        if (!Enabled) {
            return;
        }

        Player.velocity.Y += 0135.1f;
    }

    public override void PostUpdate() {
        base.PostUpdate();

        if (!Enabled) {
            return;
        }

        Player.noFallDmg = true;

        Player.velocity.Y = 0.1f;
    }

    public override void HideDrawLayers(PlayerDrawSet drawInfo) {
        base.HideDrawLayers(drawInfo);

        if (!Enabled) {
            return;
        }

        PlayerDrawLayers.HeldItem.Hide();
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) {
        base.ModifyDrawInfo(ref drawInfo);

        if (!Enabled) {
            return;
        }

        var drawPlayer = drawInfo.drawPlayer;

        if (!drawPlayer.TryGetModPlayer(out DrawInfoPlayer drawInfoPlayer)) {
            return;
        }

        drawInfoPlayer.AddModifier(new GliderDrawModifier(0.2f));
    }
}
