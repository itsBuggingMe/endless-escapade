using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EndlessEscapade.Utilities;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.EC;

public sealed class ProjectileComponentLoader : ModSystem
{
    private static readonly Dictionary<Type, ProjectileComponent> Components = [];

    public override void Load() {
        base.Load();

        LoadComponents();
    }

    public static bool HasRequirements<T>(Projectile projectile, T component) where T : ProjectileComponent {
        var type = component.GetType();

        if (!RequirementLoader.TryGetRequirements(type, out var requirements)) {
            return true;
        }

        var success = true;

        foreach (var requirement in requirements) {
            var hasInstance = Components.TryGetValue(requirement, out var instance);
            var hasRequirement = projectile.TryGetGlobalProjectile(instance, out var result) && result.Enabled;

            if (!hasInstance || !hasRequirement) {
                success = false;
                break;
            }
        }

        return success;
    }

    private void LoadComponents() {
        var components = new List<ProjectileComponent>();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(ProjectileComponent))) {
                continue;
            }

            var attribute = type.GetCustomAttribute<AutoloadAttribute>();

            if (attribute?.Value == true) {
                continue;
            }

            var instance = (ProjectileComponent)Activator.CreateInstance(type, true);

            components.Add(instance);

            Components[type] = instance;
        }

        components.Sort(static (first, other) => {
            var firstDependency = first.GetType().GetCustomAttribute<AfterAttribute>();
            var otherDependency = other.GetType().GetCustomAttribute<AfterAttribute>();

            if (firstDependency != null && firstDependency.Type == other.GetType()) {
                return 1;
            }

            if (otherDependency != null && otherDependency.Type == first.GetType()) {
                return -1;
            }

            return 0;
        });

        LoaderUtils.ForEachAndAggregateExceptions(components, component => Mod.AddContent(component));
    }
}
