using EndlessEscapade.Core.Ambience;
using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class WoodFootsteps : ModFootstep
{
    public override SoundStyle Sound { get; } = new("EndlessEscapade/Assets/Sounds/Ambience/Footsteps/Wood/Wood", 5, SoundType.Ambient) {
        Volume = 0.2f,
        PitchVariance = 0.25f,
        SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
    };

    public override string Material { get; } = "Wood";
}