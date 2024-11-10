using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ClientConfiguration : ModConfig
{
    /// <summary>
    ///     Whether inventory item effects are enabled or not.
    /// </summary>
    [Header("Graphics")]
    [DefaultValue(true)]
    public bool EnableInventoryItemEffects { get; set; } = true;
}
