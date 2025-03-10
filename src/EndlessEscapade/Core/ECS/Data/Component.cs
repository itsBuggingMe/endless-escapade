using EndlessEscapade.Core.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EndlessEscapade.Core.ECS.Data;
internal class Component<T>
{
    public static readonly ComponentID ID = Component.GetComponentID<T>();
}

internal static class Component
{
    private static int nextComponentID = 1;
    private static Dictionary<Type, ComponentID> existingComponentIDs = [];
    internal static FastStack<(Type UnderlyingType, Func<ComponentStorage> StorageFactory)> ComponentMetaDataTable = new();
    public static ComponentStorage Create(ComponentID componentID) => ComponentMetaDataTable[componentID.GetRawValue()].StorageFactory();

    static Component()
    {
        ComponentMetaDataTable.Push((typeof(void), () => throw new NotSupportedException("Cannot create component storage of default(ComponentID)")));
    }

    public static ComponentID GetComponentID<T>()
    {
        ref ComponentID elem = ref CollectionsMarshal.GetValueRefOrAddDefault(existingComponentIDs, typeof(T), out var exists);
        if (exists)
            return elem;

        ComponentMetaDataTable.Push((typeof(T), () => new ComponentStorage<T>()));

        return elem = ComponentID.CreateFromRawValue((ushort)nextComponentID++);
    }
}