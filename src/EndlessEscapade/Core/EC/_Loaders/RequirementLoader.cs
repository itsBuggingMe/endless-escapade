using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.EC;

public sealed class RequirementLoader : ModSystem
{
    private static readonly Dictionary<Type, List<Type>> Requirements = [];

    public override void Load() {
        base.Load();

        LoadRequirements();
    }

    public static bool TryGetRequirements(Type type, out IReadOnlyList<Type> result) {
        if (Requirements.TryGetValue(type, out var requirements)) {
            result = requirements;
            return true;
        }

        result = [];

        return false;
    }

    private void LoadRequirements() {
        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract) {
                continue;
            }

            var attributes = type.GetCustomAttributes<RequiresAttribute>();

            if (attributes.Count() <= 0) {
                continue;
            }

            foreach (var attribute in attributes) {
                if (!Requirements.ContainsKey(type)) {
                    Requirements[type] = new List<Type>();
                }

                Requirements[type].Add(attribute.Type);
            }
        }
    }
}
