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

        LoadComponents();
        LoadRequirements();
    }

    public static bool HasDependencies(Projectile projectile, Type type) {
        if (!Dependencies.TryGetValue(type, out var dependencies)) {
            return true;
        }

        var enabled = true;

        foreach (var requirement in dependencies) {

        }

        return enabled;
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

    private void LoadRequirements() {
        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(ProjectileComponent))) {
                continue;
            }

            var attributes = type.GetCustomAttributes<RequiresAttribute>();

            if (attributes == null) {
                continue;
            }

            if (!Dependencies.ContainsKey(type)) {
                Dependencies[type] = new List<Type>();
            }

            foreach (var requirement in attributes) {
                Dependencies[type].Add(requirement.Type);
            }
        }
    }
}
