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

    public static bool HasDependencies(Type type, Projectile projectile) {
        return true;
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

            var instance = (ProjectileComponent)Activator.CreateInstance(type);

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

        foreach (var component in components) {
            Mod.AddContent(component);
        }
    }

    private void LoadRequirements() {
        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(ProjectileComponent))) {
                continue;
            }

            var attribute = type.GetCustomAttribute<RequiresAttribute>();

            if (attribute == null) {
                continue;
            }

            if (!Dependencies.ContainsKey(type)) {
                Dependencies[type] = new List<Type>();
            }

            Dependencies[type].Add(attribute.Type);
        }
    }
}
