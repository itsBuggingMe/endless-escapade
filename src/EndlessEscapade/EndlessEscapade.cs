namespace EndlessEscapade;

/// <summary>
///     The <see cref="Mod"/> implementation of Endless Escapade.
/// </summary>
public sealed partial class EndlessEscapade : Mod
{
    /// <summary>
    ///    Gets the <see cref="Mod"/> implementation of Endless Escapade.
    /// </summary>
    public static EndlessEscapade Instance => ModContent.GetInstance<EndlessEscapade>();
}