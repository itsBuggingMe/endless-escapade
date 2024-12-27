using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.ECS;

public sealed partial class ComponentSystem : ModSystem
{
    private static partial class ComponentData<T> where T : struct
    {
        public static readonly int Id = ComponentTypeCount++;
    }
    
    internal const byte MaskSize = BitsUtils.ULONG_SIZE_IN_BITS;
    
    public static int ComponentTypeCount { get; private set; }
}