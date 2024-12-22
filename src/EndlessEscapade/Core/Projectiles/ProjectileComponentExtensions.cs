// Kept outside of the Utilities/_Extensions/ scope for convenience when using components.

using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Projectiles;

public static class ProjectileComponentExtensions
{
    public static bool TryEnable<T>(this Projectile projectile, [MaybeNullWhen(false)] out T result) where T : ProjectileComponent
    {
        if (!projectile.TryGetGlobalProjectile(out result))
        {
            return false;
        }

        result.Enabled = true;

        return true;
    }
}