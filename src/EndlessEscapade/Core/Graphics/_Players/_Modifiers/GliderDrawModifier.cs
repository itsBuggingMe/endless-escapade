using Terraria.DataStructures;

namespace EndlessEscapade.Core.Graphics;

// TODO: Split up between multiple modifiers.
public sealed class GliderDrawModifier(float smoothness) : IPlayerDrawModifier
{
    // The 'oldDirection' field from 'Entity' gets set after we apply our logic, so we have to keep track of it ourselves.
    private int oldDirection;

    void IPlayerDrawModifier.ModifyDrawInfo(ref PlayerDrawSet drawInfo) {
        var drawPlayer = drawInfo.drawPlayer;

        drawPlayer.legFrame.Y = 0;

        drawPlayer.itemRotation = -MathHelper.PiOver2;

        if (drawPlayer.direction == -1) {
            drawPlayer.itemRotation -= MathHelper.Pi;
        }

        var xOffset = drawPlayer.velocity.X * 0.05f;
        var yOffset = MathF.Sin(Main.GameUpdateCount * 0.1f) * drawPlayer.velocity.Y * 0.1f;

        var targetFullRotation = MathHelper.PiOver2 + xOffset + yOffset;

        if (drawPlayer.direction == -1) {
            targetFullRotation -= MathHelper.Pi;
        }

        if (drawPlayer.direction != oldDirection) {
            drawPlayer.fullRotation = targetFullRotation;
        }
        else {
            drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(targetFullRotation, smoothness);
        }

        oldDirection = drawPlayer.direction;

        drawPlayer.fullRotationOrigin = drawPlayer.Size / 2f;

        var targetHeadRotation = drawPlayer.AngleTo(Main.MouseWorld);

        var minHeadRotation = MathHelper.ToRadians(-40f);
        var maxHeadRotation = MathHelper.ToRadians(40f);

        // TODO: Make an utility/extension for this. 'AngleClamp'.
        if (drawPlayer.direction == -1) {
            minHeadRotation = MathHelper.ToRadians(40f) - MathHelper.Pi;
            maxHeadRotation = MathHelper.ToRadians(320f) - MathHelper.Pi;

            if (targetHeadRotation > minHeadRotation && targetHeadRotation < maxHeadRotation) {
                var distanceToMin = MathF.Abs(targetHeadRotation - minHeadRotation);
                var distanceToMax = MathF.Abs(targetHeadRotation - maxHeadRotation);

                if (distanceToMin < distanceToMax) {
                    targetHeadRotation = minHeadRotation;
                } else {
                    targetHeadRotation = maxHeadRotation;
                }
            }

            targetHeadRotation += MathHelper.Pi;
        }
        else {
            targetHeadRotation = MathHelper.Clamp(targetHeadRotation, minHeadRotation, maxHeadRotation);
        }

        drawPlayer.headRotation = targetHeadRotation;

        var targetArmRotation = drawPlayer.AngleTo(Main.MouseWorld);

        var minArmRotation = MathHelper.ToRadians(-10f);
        var maxArmRotation = MathHelper.ToRadians(10f);

        if (drawPlayer.direction == -1) {
            minArmRotation = MathHelper.ToRadians(10f) - MathHelper.Pi;
            maxArmRotation = MathHelper.ToRadians(350f) - MathHelper.Pi;

            if (targetArmRotation > minArmRotation && targetArmRotation < maxArmRotation) {
                var distanceToMin = MathF.Abs(targetArmRotation - minArmRotation);
                var distanceToMax = MathF.Abs(targetArmRotation - maxArmRotation);

                if (distanceToMin < distanceToMax) {
                    targetArmRotation = minArmRotation;
                } else {
                    targetArmRotation = maxArmRotation;
                }
            }

            targetArmRotation += MathHelper.Pi;
        }
        else {
            targetArmRotation = MathHelper.Clamp(targetArmRotation, minArmRotation, maxArmRotation);
        }

        drawPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, targetArmRotation + MathHelper.Pi);
        drawPlayer.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, targetArmRotation + MathHelper.Pi);

        var rotationOffset = new Vector2(0f, -4f).RotatedBy(targetArmRotation);
        var positionOffset = new Vector2(16f * -drawPlayer.direction, 0f);

        drawPlayer.itemLocation = drawPlayer.Center + rotationOffset + positionOffset;
    }
}
