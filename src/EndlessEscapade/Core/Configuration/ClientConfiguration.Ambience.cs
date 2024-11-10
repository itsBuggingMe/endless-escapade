using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ClientConfiguration : ModConfig
{
    /// <summary>
    ///		Whether ambience tracks are enabled or not.
    /// </summary>
    [Header("Ambience")]
    [DefaultValue(true)]
    public bool EnableAmbienceTracks { get; set; } = true;

    /// <summary>
    ///		Whether ambience sounds are enabled or not.
    /// </summary>
    [DefaultValue(true)]
    public bool EnableAmbienceSounds { get; set; } = true;

    /// <summary>
    ///     Whether footsteps are enabled or not.
    /// </summary>
    [DefaultValue(true)]
    public bool EnableFootsteps { get; set; } = true;
}
