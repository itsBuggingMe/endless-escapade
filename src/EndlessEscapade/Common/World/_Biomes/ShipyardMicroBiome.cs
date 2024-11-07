using EndlessEscapade.Content.NPCs.Shipyard;
using EndlessEscapade.Utilities;
using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class ShipyardMicroBiome : MicroBiome
{
    /// <summary>
    ///     The horizontal offset to the first deck pillar, in tiles, relative to the origin.
    /// </summary>
    public const int FIRST_DECK_PILLAR_OFFSET_X = 4;

    /// <summary>
    ///     The horizontal offset to the second deck pillar, in tiles, relative to the origin.
    /// </summary>
    public const int SECOND_DECK_PILLAR_OFFSET_X = 20;

    /// <summary>
    ///     The horizontal offset to the third deck pillar, in tiles, relative to the origin.
    /// </summary>
    public const int THIRD_DECK_PILLAR_OFFSET_X = 36;

    /// <summary>
    ///     The horizontal offset to the first house pillar, in tiles, relative to the origin.
    /// </summary>
    public const int FIRST_HOUSE_PILLAR_OFFSET_X = 56;

    /// <summary>
    ///     The horizontal offset to the second house pillar, in tiles, relative to the origin.
    /// </summary>
    public const int SECOND_HOUSE_PILLAR_OFFSET_X = 74;

    /// <summary>
    ///     The vertical offset to each deck pillar, in tiles, relative to the origin.
    /// </summary>
    public const int DECK_PILLAR_OFFSET_Y = 38;

    /// <summary>
    ///     The vertical offset to each house pillar, in tiles, relative to the origin.
    /// </summary>
    public const int HOUSE_PILLAR_OFFSET_Y = 26;

    /// <summary>
    ///     The width of each pillar, in tiles.
    /// </summary>
    public const int PILLAR_WIDTH = 2;

    /// <summary>
    ///     The horizontal offset to the Sailor's room, in tiles, relative to the origin.
    /// </summary>
    public const int SAILOR_ROOM_OFFSET_X = 60;

    /// <summary>
    ///     The vertical offset to the Sailor's room, in tiles, relative to the origin.
    /// </summary>
    public const int SAILOR_ROOM_OFFSET_Y = 10;

    public override bool Place(Point origin, StructureMap structures) {
        var mod = EndlessEscapade.Instance;
        var dims = Point16.Zero;

        if (!Generator.GetDimensions("Assets/Structures/Shipyard", mod, ref dims)) {
            return false;
        }

        var originOffset = new Point16(dims.X / 2, dims.Y - dims.Y / 3);
        var newOrigin = new Point16(origin.X, origin.Y) - originOffset;

        var placement = structures.CanPlace(new Rectangle(newOrigin.X, newOrigin.Y, dims.X, dims.Y));

        if (!placement) {
            return false;
        }

        var generated = Generator.GenerateStructure("Assets/Structures/Shipyard", newOrigin, mod);

        if (!generated) {
            return false;
        }

        structures.AddProtectedStructure(new Rectangle(newOrigin.X, newOrigin.Y, dims.X, dims.Y));

        for (var j = newOrigin.Y + 30; j < newOrigin.Y + dims.Y; j++) {
            var offset = WorldGen.genRand.Next(-4, 4);

            var strength = WorldGen.genRand.Next(10, 17);
            var steps = WorldGen.genRand.Next(1, 4);

            WorldGen.TileRunner(newOrigin.X + dims.X + offset, j, strength, steps, TileID.Sand, true);
        }

        for (var i = 0; i < PILLAR_WIDTH; i++) {
            WorldGenerationUtils.ExtendDownwards(newOrigin.X + FIRST_DECK_PILLAR_OFFSET_X + i, newOrigin.Y + DECK_PILLAR_OFFSET_Y);
            WorldGenerationUtils.ExtendDownwards(newOrigin.X + SECOND_DECK_PILLAR_OFFSET_X + i, newOrigin.Y + DECK_PILLAR_OFFSET_Y);
            WorldGenerationUtils.ExtendDownwards(newOrigin.X + THIRD_DECK_PILLAR_OFFSET_X + i, newOrigin.Y + DECK_PILLAR_OFFSET_Y);

            WorldGenerationUtils.ExtendDownwards(newOrigin.X + FIRST_HOUSE_PILLAR_OFFSET_X + i, newOrigin.Y + HOUSE_PILLAR_OFFSET_Y);
            WorldGenerationUtils.ExtendDownwards(newOrigin.X + SECOND_HOUSE_PILLAR_OFFSET_X + i, newOrigin.Y + HOUSE_PILLAR_OFFSET_Y);
        }

        var sailorX = (int)((newOrigin.X + SAILOR_ROOM_OFFSET_X) * 16f);
        var sailorY = (int)((newOrigin.Y + SAILOR_ROOM_OFFSET_Y) * 16f);

        var npc = NPC.NewNPCDirect(new EntitySource_WorldGen(), sailorX, sailorY, ModContent.NPCType<SailorNPC>());

        npc.UpdateHomeTileState(false, (int)(sailorX / 16f), (int)(sailorY / 16f));

        return true;
    }
}
