using Microsoft.Xna.Framework.Audio;

namespace EndlessEscapade.Framework.Core;

[Autoload(Side = ModSide.Client)]
public abstract class ModAudioFilter : ModType
{
    public abstract void Apply(SoundEffectInstance instance, in AudioParameters parameters);

    protected sealed override void Register()
    {
        ModTypeLookup<ModAudioFilter>.Register(this);
    }
}