using System.Collections.Generic;
using EndlessEscapade.Utilities;
using EndlessEscapade.Utilities.Extensions;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class ShipyardSystem : ModSystem
{
    /// <summary>
    ///     The unique identifier for the Shipyard's <see cref="PassLegacy"/> added during world
    ///     generation in <see cref="ModifyWorldGenTasks"/>.
    /// </summary>
    public const string SHIPYARD_PASS_NAME = $"{nameof(EndlessEscapade)}:{nameof(ShipyardMicroBiome)}";

    public const int SAILBOAT_DISTANCE = 80;

    /// <summary>
    ///     Whether the Sailboat is repaired or not.
    /// </summary>
    public bool Repaired { get; private set; }

    /// <summary>
    ///     The placement origin of the Shipyard, in tile coordinates.
    /// </summary>
    public static Point ShipyardOrigin { get; private set; }

    /// <summary>
    ///     The placement origin of the Sailboat, in tile coordinates.
    /// </summary>
    public static Point SailboatOrigin { get; private set; }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
        base.ModifyWorldGenTasks(tasks, ref totalWeight);

        var index = tasks.FindIndex(static pass => pass.Name == "Final Cleanup");

        if (index == -1) {
            return;
        }

        tasks.Insert(index + 1, new PassLegacy(SHIPYARD_PASS_NAME, GenerateShipyard));
    }

    public override void ClearWorld() {
        base.ClearWorld();

        Repaired = false;

        ShipyardOrigin = Point.Zero;
        SailboatOrigin = Point.Zero;
    }

    public override void SaveWorldData(TagCompound tag) {
        base.SaveWorldData(tag);

        tag["repaired"] = Repaired;

        tag["shipyardOrigin"] = ShipyardOrigin;
        tag["sailboatOrigin"] = SailboatOrigin;
    }

    public override void LoadWorldData(TagCompound tag) {
        base.LoadWorldData(tag);

        Repaired = tag.GetBool("repaired");

        ShipyardOrigin = tag.Get<Point>("shipyardOrigin");
        SailboatOrigin = tag.Get<Point>("sailboatOrigin");
    }

    private void GenerateShipyard(GenerationProgress progress, GameConfiguration configuration) {
        progress.Message = Mod.GetLocalizationValue("UI.Generation.Shipyard");

        var foundOcean = false;

        var startX = 0;
        var startY = (int)(Main.worldSurface * 0.35f);

        while (!foundOcean) {
            var tile = Framing.GetTileSafely(startX, startY);

            if (tile.HasLiquidType(LiquidID.Water) && tile.HasLiquidAmount(byte.MaxValue)) {
                foundOcean = true;
                break;
            }

            startY++;
        }

        var foundBeach = false;

        while (!foundBeach) {
            var tile = Framing.GetTileSafely(startX, startY);

            if (tile.HasTileType(TileID.Sand) && tile.IsSolid()) {
                foundBeach = true;
                break;
            }

            startX++;
        }

        if (!foundOcean || !foundBeach) {
            return;
        }

        var biggestY = startY;

        for (var i = startX; i < startX + 50; i++) {
            for (var j = 0; j < Main.maxTilesY; j++) {
                var tile = Framing.GetTileSafely(i, j);

                if (tile.HasTileType(TileID.Sand) && !tile.HasAnyLiquidAmount() && j < biggestY) {
                    biggestY = j;
                    break;
                }
            }
        }

        ShipyardOrigin = new Point(startX, biggestY);
        SailboatOrigin = new Point(startX - SAILBOAT_DISTANCE, biggestY);

        var shipyard = GenVars.configuration.CreateBiome<ShipyardMicroBiome>();
        var sailboat = GenVars.configuration.CreateBiome<BrokenSailboatMicroBiome>();

        shipyard.Place(ShipyardOrigin, GenVars.structures);
        sailboat.Place(SailboatOrigin, GenVars.structures);
    }
}
