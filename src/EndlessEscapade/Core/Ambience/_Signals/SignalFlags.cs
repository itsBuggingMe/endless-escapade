using EndlessEscapade.Content.Biomes;
using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Ambience;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Underwater(in AmbienceContext context)
    {
        return context.Player.IsUnderwater();
    }

    [SignalUpdater]
    public static bool Beach(in AmbienceContext context)
    {
        return context.Player.ZoneBeach;
    }

    [SignalUpdater]
    public static bool Shipyard(in AmbienceContext context)
    {
        return context.Player.InModBiome<ShipyardBiome>();
    }

    [SignalUpdater]
    public static bool Forest(in AmbienceContext context)
    {
        return context.Player.ZonePurity;
    }

    [SignalUpdater]
    public static bool Day(in AmbienceContext context)
    {
        return Main.dayTime;
    }

    [SignalUpdater]
    public static bool Night(in AmbienceContext context)
    {
        return !Main.dayTime;
    }

    [SignalUpdater]
    public static bool Lava(in AmbienceContext context)
    {
        return context.Metrics.GetLiquidCount(LiquidID.Lava) > 50;
    }

    [SignalUpdater]
    public static bool Underground(in AmbienceContext context)
    {
        return context.Player.ZoneDirtLayerHeight;
    }

    [SignalUpdater]
    public static bool Surface(in AmbienceContext context)
    {
        return context.Player.ZoneOverworldHeight && !context.Player.ZoneUndergroundDesert;
    }
}