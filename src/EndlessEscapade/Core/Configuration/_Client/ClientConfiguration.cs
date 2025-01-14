using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ClientConfiguration : ModConfig
{
    /// <summary>
    ///     Gets the <see cref="ModConfig"/> implementation of Endless Escapade's client-side configuration.
    /// </summary>
    public static ClientConfiguration Instance => ModContent.GetInstance<ClientConfiguration>();

    public override ConfigScope Mode { get; } = ConfigScope.ClientSide;
}