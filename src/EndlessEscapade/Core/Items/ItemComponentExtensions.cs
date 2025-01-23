// Kept outside of the Utilities/_Extensions/ scope for convenience when using components.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EndlessEscapade.Core.Items;

public static class ItemComponentExtensions
{
    public static bool TryEnable<T>(this Item projectile, [NotNullWhen(true)] out T? component) where T : ItemComponent
    {
        if (!projectile.TryGetGlobalItem(out component))
        {
            return false;
        }
        component!.Enabled = true;

        return true;
    }
}