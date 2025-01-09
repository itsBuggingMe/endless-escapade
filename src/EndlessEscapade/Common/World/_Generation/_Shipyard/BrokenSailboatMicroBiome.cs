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

    public override bool Place(Point origin, StructureMap structures)
    {
        var mod = EndlessEscapade.Instance;
        var dims = Point16.Zero;

        var hasSailboatDimensions = Generator.GetDimensions(BROKEN_SAILBOAT_ASSET_PATH, mod, ref dims);

        if (!hasSailboatDimensions)
        {
            return false;
        }

        origin -= new Point(dims.X / 2, dims.Y - 10);

        var canPlaceSailboat = structures.CanPlace(new Rectangle(origin.X, origin.Y, dims.X, dims.Y));

        if (!canPlaceSailboat)
        {
            return false;
        }

        var generatedSailboat = Generator.GenerateStructure(BROKEN_SAILBOAT_ASSET_PATH, new Point16(origin.X, origin.Y), mod);

        if (!generatedSailboat)
        {
            return false;
        }

        structures.AddProtectedStructure(new Rectangle(origin.X, origin.Y, dims.X, dims.Y));

        return true;
    }
}