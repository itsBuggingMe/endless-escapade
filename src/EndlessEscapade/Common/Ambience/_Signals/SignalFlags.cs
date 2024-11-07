using EndlessEscapade.Content.Biomes;
using EndlessEscapade.Utilities.Extensions;

namespace EndlessEscapade.Common.Ambience;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Underwater(in AmbienceContext context) {
        return context.Player.IsUnderwater();
    }

    [SignalUpdater]
    public static bool Beach(in AmbienceContext context) {
        return context.Player.ZoneBeach;
    }

    [SignalUpdater]
    public static bool Shipyard(in AmbienceContext context) {
        return context.Player.InModBiome<ShipyardBiome>();
    }

    [SignalUpdater]
    public static bool Forest(in AmbienceContext context) {
        return context.Player.ZoneForest;
    }

    [SignalUpdater]
    public static bool Day(in AmbienceContext context) {
        return Main.dayTime;
    }

    [SignalUpdater]
    public static bool Night(in AmbienceContext context) {
        return !Main.dayTime;
    }
}
