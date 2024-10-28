using System.Reflection;

namespace EndlessEscapade.Utilities;

/// <summary>
///     Provides <see cref="Projectile"/> extensions.
/// </summary>
public static class ProjectileExtensions
{
    private static readonly MethodInfo TryGetGlobalProjectileCallback = typeof(Projectile)
        .GetMethod("TryGetGlobalProjectile", BindingFlags.Instance | BindingFlags.Public)
        .MakeGenericMethod(typeof(Type));

    public static bool HasGlobalProjectile(this Projectile projectile, Type type) {
        if (!typeof(GlobalProjectile).IsAssignableFrom(type)) {
            return false;
        }

        return TryGetGlobalProjectileCallback.Invoke(projectile, [type]) is bool value ? value : false;
    }
}
