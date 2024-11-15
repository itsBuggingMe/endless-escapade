using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ClientConfiguration : ModConfig
{
    /// <summary>
    ///     Whether inventory hover item effects are enabled or not.
    /// </summary>
    [Header("Graphics")]
    [DefaultValue(true)]
    public bool EnableInventoryHoverItemEffects { get; set; } = true;

    /// <summary>
    ///     Whether inventory position item effects are enabled or not.
    /// </summary>
    [DefaultValue(true)]
    public bool EnableInventoryPositionItemEffects { get; set; } = true;
}
