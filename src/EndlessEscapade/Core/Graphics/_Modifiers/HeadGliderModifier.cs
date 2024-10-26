using Terraria.DataStructures;

namespace EndlessEscapade.Core.Graphics;

public sealed class HeadGliderModifier : IPlayerDrawModifier
{
    void IPlayerDrawModifier.ModifyDrawInfo(ref PlayerDrawSet drawInfo) {
        var drawPlayer = drawInfo.drawPlayer;

        var targetHeadRotation = drawPlayer.AngleTo(Main.MouseWorld);

        var minHeadRotation = MathHelper.ToRadians(-40f);
        var maxHeadRotation = MathHelper.ToRadians(40f);

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
    }
}
