using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.ECS;

public sealed class SystemCallbackSystem : ModSystem
{
    public override void Load() {
        base.Load();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            if (type.IsAbstract || type.IsValueType) {
                continue;
            }

            foreach (var method in type.GetMethods(BindingFlags.Static)) {
                var attribute = method.GetCustomAttribute<SystemCallbackAttribute>();

                if (attribute == null) {
                    continue;
                }


            }
        }
    }
}
