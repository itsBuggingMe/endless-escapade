using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ServerConfiguration : ModConfig
{
    [Header("Recipes")]
    [ReloadRequired]
    [DefaultValue(true)]
    public bool EnableAccessoryRecipes { get; set; } = true;
    
    [ReloadRequired]
    [DefaultValue(true)]
    public bool EnableConsumableRecipes { get; set; } = true;
    
    [ReloadRequired]
    [DefaultValue(true)]
    public bool EnableWeaponRecipes { get; set; } = true;
}