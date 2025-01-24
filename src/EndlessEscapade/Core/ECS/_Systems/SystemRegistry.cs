using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.ECS;

public sealed class SystemRegistry 
{
    public void Initialize(Assembly assembly)
    {
        foreach (var type in AssemblyManager.GetLoadableTypes(assembly))
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = method.GetCustomAttribute<SystemAttribute>();

                if (attribute == null)
                {
                    continue;
                }
                
                if (!method.IsStatic)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}