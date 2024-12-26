using Terraria.DataStructures;

namespace EndlessEscapade.Content.Items.Gliders;

public sealed class GliderPlayer : ModPlayer
{
    public bool Enabled { get; set; }
    
    public override void ResetEffects()
    {
        base.ResetEffects();

        Enabled = false;
    }

    public override void PostUpdate()
    {
        base.PostUpdate();
        
        if (!Enabled) 
        {
            return;
        }

        Player.noFallDmg = true;

        Player.velocity.Y += 0.01f;

        if (Player.velocity.Y <= 0.1f)
        {
            return;
        }

        Player.velocity.Y = 0.1f;
    }
    
    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) 
    {
        base.ModifyDrawInfo(ref drawInfo);

        if (!Enabled)
        {
            return;
        }

        var player = drawInfo.drawPlayer;

        player.legFrame.Y = 5 * player.legFrame.Height;

        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi - MathHelper.ToRadians(15f) * -player.direction);
        player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi - MathHelper.ToRadians(30f) * -player.direction);

        player.fullRotation = MathF.Sin(Main.GameUpdateCount * 0.1f) * 0.05f * (player.velocity.Length() + 1f);
    }
}