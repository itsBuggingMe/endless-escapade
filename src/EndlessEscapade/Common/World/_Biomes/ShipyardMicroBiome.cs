using EndlessEscapade.Content.NPCs.Shipyard;
using EndlessEscapade.Utilities;
using StructureHelper;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class ShipyardMicroBiome : MicroBiome
{
    private const int FIRST_DECK_PILLAR_OFFSET_X = 4;
    private const int SECOND_DECK_PILLAR_OFFSET_X = 20;
    private const int THIRD_DECK_PILLAR_OFFSET_X = 36;

    private const int FIRST_HOUSE_PILLAR_OFFSET_X = 56;
    private const int SECOND_HOUSE_PILLAR_OFFSET_X = 74;

    private const int DECK_PILLAR_OFFSET_Y = 38;
    private const int HOUSE_PILLAR_OFFSET_Y = 26;

    private const int PILLAR_WIDTH = 2;

    private const int SAILOR_ROOM_OFFSET_X = 60;
    private const int SAILOR_ROOM_OFFSET_Y = 10;

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
