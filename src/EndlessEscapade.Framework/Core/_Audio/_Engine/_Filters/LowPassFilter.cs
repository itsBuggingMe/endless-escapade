using Microsoft.Xna.Framework.Audio;

namespace EndlessEscapade.Framework.Core;

public sealed class LowPassFilter : ModAudioFilter
{
    public override void Apply(SoundEffectInstance instance, in AudioParameters parameters)
    {
        var intensity = parameters.LowPass;

        if (intensity <= 0f)
        {
            return;
        }

        instance.INTERNAL_applyLowPassFilter(1f - intensity);
    }
}