using System.Collections.Generic;
using System.Reflection;
using EndlessEscapade.Utilities;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.EC;

public sealed class ProjectileComponentSystem : ModSystem
{
    private static readonly Dictionary<Type, List<Type>> Dependencies = [];

    public override void Load() {
        base.Load();

        LoadRequirements();
    }

    public static bool HasDependencies(Type type, Projectile projectile) {
        if (!Dependencies.TryGetValue(type, out var dependencies)) {
            return true;
        }

        var valid = true;

        foreach (var dependency in dependencies) {
            if (!projectile.HasGlobalProjectile(type)) {
                valid = false;
                break;
            }
        }

        return valid;
    }

    private void LoadRequirements() {
        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(ProjectileComponent))) {
                continue;
            }

            var attribute = type.GetCustomAttribute<RequiredAttribute>();

            if (attribute == null) {
                continue;
            }

            Dependencies[type] ??= new List<Type>();
            Dependencies[type].Add(attribute.Type);
        }
    }
}
