using EndlessEscapade.Core.Ambience;
using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class BeachWavesTrack : ModAmbienceTrack
{
    public override SoundStyle Sound { get; } = new("EndlessEscapade/Assets/Sounds/Ambience/Tracks/Beach/BeachWavesLoop", SoundType.Ambient)
    {
        Volume = 1f,
        IsLooped = true
    };

    public override float StepIn { get; } = 0.01f;

    public override float StepOut { get; } = 0.01f;

    public override bool IsAmbienceActive(in AmbienceContext context)
    {
        return SignalsSystem.GetSignal("Beach", "Shipyard", "Surface");
    }
}