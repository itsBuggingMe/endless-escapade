using EndlessEscapade.Content.Biomes;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Ambience;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Underwater(in AmbienceContext context)
        => context.Player.IsUnderwater();

    [SignalUpdater]
    public static bool Beach(in AmbienceContext context)
        => context.Player.ZoneBeach;

    [SignalUpdater]
    public static bool Shipyard(in AmbienceContext context)
        => context.Player.InModBiome<ShipyardBiome>();

    [SignalUpdater]
    public static bool Forest(in AmbienceContext context)
        => context.Player.ZonePurity;

    [SignalUpdater]
    public static bool Day(in AmbienceContext context)
        => Main.dayTime;

    [SignalUpdater]
    public static bool Night(in AmbienceContext context)
        => !Main.dayTime;

    [SignalUpdater]
    public static bool Lava(in AmbienceContext context)
        => context.Metrics.GetLiquidCount(LiquidID.Lava) > 50;

    [SignalUpdater]
    public static bool Underground(in AmbienceContext context)
        => context.Player.ZoneDirtLayerHeight;

    [SignalUpdater]
    public static bool Surface(in AmbienceContext context)
        => context.Player.ZoneOverworldHeight && !context.Player.ZoneUndergroundDesert;
}