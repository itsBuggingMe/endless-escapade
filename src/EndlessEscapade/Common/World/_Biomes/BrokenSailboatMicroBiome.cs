using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class BrokenSailboatMicroBiome : MicroBiome
{
    /// <summary>
    ///     The path to the Broken Sailboat structure file, not qualified by the mod's internal name.
    /// </summary>
    public const string BROKEN_SAILBOAT_ASSET_PATH = "Assets/Structures/BrokenSailboat";

    public override bool Place(Point origin, StructureMap structures) {
        var mod = EndlessEscapade.Instance;
        var dims = Point16.Zero;

        if (!Generator.GetDimensions(BROKEN_SAILBOAT_ASSET_PATH, mod, ref dims)) {
            return false;
        }

        var placement = structures.CanPlace(new Rectangle(origin.X, origin.Y, dims.X, dims.Y));

        if (!placement) {
            return false;
        }

        var generated = Generator.GenerateStructure(BROKEN_SAILBOAT_ASSET_PATH, new Point16(origin.X, origin.Y), mod);

        if (!generated) {
            return false;
        }

        structures.AddProtectedStructure(new Rectangle(origin.X, origin.Y, dims.X, dims.Y));

        return true;
    }
}
