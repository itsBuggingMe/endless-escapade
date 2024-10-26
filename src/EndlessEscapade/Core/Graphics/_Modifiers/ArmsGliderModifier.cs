using Terraria.DataStructures;

namespace EndlessEscapade.Core.Graphics;

public sealed class ArmsGliderModifier : IPlayerDrawModifier
{
    void IPlayerDrawModifier.ModifyDrawInfo(ref PlayerDrawSet drawInfo) {
        var drawPlayer = drawInfo.drawPlayer;

        var targetArmRotation = drawPlayer.AngleTo(Main.MouseWorld);

        var minHeadRotation = MathHelper.ToRadians(-30f);
        var maxHeadRotation = MathHelper.ToRadians(30f);

        if (drawPlayer.direction == -1) {
            minHeadRotation = MathHelper.ToRadians(40f) - MathHelper.Pi;
            maxHeadRotation = MathHelper.ToRadians(320f) - MathHelper.Pi;

            if (targetArmRotation > minHeadRotation && targetArmRotation < maxHeadRotation) {
                var distanceToMin = MathF.Abs(targetArmRotation - minHeadRotation);
                var distanceToMax = MathF.Abs(targetArmRotation - maxHeadRotation);

                if (distanceToMin < distanceToMax) {
                    targetArmRotation = minHeadRotation;
                } else {
                    targetArmRotation = maxHeadRotation;
                }
            }

            targetArmRotation += MathHelper.Pi;
        }
        else {
            targetArmRotation = MathHelper.Clamp(targetArmRotation, minHeadRotation, maxHeadRotation);
        }

        drawPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, targetArmRotation + MathHelper.Pi);
        drawPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, targetArmRotation + MathHelper.Pi);

        drawPlayer.itemRotation = -MathHelper.PiOver2;

        if (drawPlayer.direction == -1) {
            drawPlayer.itemRotation -= MathHelper.Pi;
        }

        var rotationOffset = new Vector2(0f, -4f).RotatedBy(targetArmRotation);
        var positionOffset = new Vector2(16f * -drawPlayer.direction, 0f);

        drawPlayer.itemLocation = drawPlayer.Center + rotationOffset + positionOffset;
    }
}
