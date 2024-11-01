// Extensions were placed outside of the Utilities/Extensions scope for convenience when using components.
namespace EndlessEscapade.Core.EC;

public static class ProjectileComponentExtensions
{
    public static bool TryEnableComponent<T>(this Projectile projectile) where T : ProjectileComponent {
        if (!projectile.TryGetGlobalProjectile<T>(out var component)) {
            return false;
        }

        component.Enabled = true;

        return true;
    }

    public static bool TryEnableComponent<T>(this Projectile projectile, out T component) where T : ProjectileComponent {
        if (!projectile.TryGetGlobalProjectile(out component)) {
            return false;
        }

        component.Enabled = true;

        return true;
    }
}
