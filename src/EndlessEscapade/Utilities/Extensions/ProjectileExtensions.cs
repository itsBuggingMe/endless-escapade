using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Utilities.Extensions;

public static class ProjectileExtensions
{
    public static bool TryGetGlobalProjectile(this Projectile projectile, Type type, [MaybeNullWhen(false)] out GlobalProjectile globalProjectile) {
        globalProjectile = null;

        return false;
    }
}
