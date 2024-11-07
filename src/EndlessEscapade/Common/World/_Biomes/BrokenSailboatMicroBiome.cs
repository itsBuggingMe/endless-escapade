using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class BrokenSailboatMicroBiome : MicroBiome
{
    /// <summary>
    ///     The path to the Broken Sailboat structure file, not qualified by the mod's internal name.
    /// </summary>
    public const string BROKEN_SAILBOAT_STRUCTURE_PATH = "Assets/Structures/BrokenSailboat";

    public override bool Place(Point origin, StructureMap structures) {
        var mod = EndlessEscapade.Instance;
        var dims = Point16.Zero;

        if (!Generator.GetDimensions(BROKEN_SAILBOAT_STRUCTURE_PATH, mod, ref dims)) {
            return false;
        }

        return true;
    }
}
