using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.EC;

public sealed class GlobalComponentSystem : ModSystem
{
    public override void Load() {
        base.Load();

        var components = new List<ProjectileComponent>();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(ProjectileComponent))) {
                continue;
            }

            var attribute = type.GetCustomAttribute<AfterAttribute>();
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
}
