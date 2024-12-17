using EndlessEscapade.Core.Ambience;
using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class SandFootsteps : ModFootstep
{
    public override SoundStyle Sound { get; } = new("EndlessEscapade/Assets/Sounds/Ambience/Footsteps/Sand/Sand", 5, SoundType.Ambient) {
        Volume = 0.4f,
        PitchVariance = 0.25f,
        SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
    };

    public override string Material { get; } = "Sand";
}