using Terraria.ModLoader.Config;

namespace EndlessEscapade.Core.Configuration;

public sealed partial class ServerConfiguration : ModConfig
{
    /// <summary>
    ///     Gets the <see cref="ModConfig"/> implementation of Endless Escapade's server-side configuration.
    /// </summary>
    public static ServerConfiguration Instance => ModContent.GetInstance<ServerConfiguration>();

    public override ConfigScope Mode { get; } = ConfigScope.ServerSide;
}