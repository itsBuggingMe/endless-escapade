using System.Runtime.CompilerServices;

namespace EndlessEscapade.Utilities.Extensions;

/// <summary>
///     Provides <see cref="Projectile"/> extension methods.
/// </summary>
public static class ProjectileExtensions
{
    /// <summary>
    ///     Transforms a projectile into a new projectile type.
    /// </summary>
    /// <remarks>
    ///     The transformation will preserve the values of <see cref="Projectile.friendly"/> and
    ///     <see cref="Projectile.hostile"/>.
    /// </remarks>
    /// <param name="projectile">The projectile to transform.</param>
    /// <param name="type">The type to transform the projectile into.</param>
    public static void Transform(this Projectile projectile, int type) {
        if (Main.netMode == NetmodeID.MultiplayerClient) {
            return;
        }

        var hostile = projectile.hostile;
        var friendly = projectile.friendly;

        projectile.SetDefaults(type);

        projectile.hostile = hostile;
        projectile.friendly = friendly;

        if (Main.netMode != NetmodeID.Server) {
            return;
        }

        projectile.netUpdate = true;

        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.whoAmI);
    }
}
