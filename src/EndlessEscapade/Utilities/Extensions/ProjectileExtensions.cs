using System.Runtime.CompilerServices;

namespace EndlessEscapade.Utilities.Extensions;

public static class ProjectileExtensions
{
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
