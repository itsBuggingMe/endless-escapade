using EndlessEscapade.Core.Ambience;
using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class BeachSeagullsSound : ModAmbienceSound
{
    public override SoundStyle Sound { get; } = new("EndlessEscapade/Assets/Sounds/Ambience/Sounds/Beach/BeachSeagulls", 2, SoundType.Ambient)
    {
        Volume = 1f,
        PitchVariance = 0.25f
    };

    public override int Chance { get; } = 200;

    public override bool IsAmbienceActive(in AmbienceContext context)
    {
        return SignalsSystem.GetSignal("Beach", "Shipyard");
    }
}