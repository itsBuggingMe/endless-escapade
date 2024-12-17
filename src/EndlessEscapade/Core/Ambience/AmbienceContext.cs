namespace EndlessEscapade.Core.Ambience;

public ref struct AmbienceContext
{
    public static AmbienceContext Default => new() {
        Player = Main.LocalPlayer,
        Metrics = Main.SceneMetrics
    };

    public Player Player { get; init; }

    public SceneMetrics Metrics { get; init; }
}