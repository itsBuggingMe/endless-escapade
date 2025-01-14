using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ClientConfiguration : ModConfig
{
	/// <summary>
	///     Whether the low pass filter is enabled or not.
	/// </summary>
	[Header("Audio")]
    [DefaultValue(true)]
    public bool EnableLowPassFilter { get; set; } = true;
}