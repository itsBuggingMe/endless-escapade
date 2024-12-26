using EndlessEscapade.Content.Biomes;
using EndlessEscapade.Core.Ambience;
using EndlessEscapade.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class BeachDolphinsSound : ModAmbienceSound
{
    public override SoundStyle Sound { get; } = new("EndlessEscapade/Assets/Sounds/Ambience/Sounds/Beach/BeachDolphins", 0, SoundType.Ambient)
    {
        Volume = 1f,
        PitchVariance = 0.25f
    };

    public override int Chance { get; } = 200;

    public override bool IsAmbienceActive(in AmbienceContext context)
    {
        return context.Player.InSurface() && (context.Player.ZoneBeach || context.Player.InModBiome<ShipyardBiome>());
    }
}