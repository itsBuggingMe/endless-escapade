using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.EC;

public sealed partial class ComponentSystem : ModSystem
{
    public override void Load() {
        base.Load();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            foreach (var method in type.GetMethods(BindingFlags.Static)) {
                var attribute = method.GetCustomAttribute<RenderCallbackAttribute>();

                if (attribute == null) {
                    continue;
                }

            }
        }
    }
}
