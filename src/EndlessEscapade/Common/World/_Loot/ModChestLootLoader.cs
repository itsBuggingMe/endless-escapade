using System.Collections.Generic;
using EndlessEscapade.Utilities.Extensions;

namespace EndlessEscapade.Common.World;

public sealed class ModChestLootLoader : ModSystem
{
    private static int flags;

    public override void PostWorldGen() {
        base.PostWorldGen();

        GenerateGuaranteedLoot();
        GenerateExtraLoot();
    }

    private static void SetFlag(int type, bool value) {
        var mask = 1 << type;

        if (value) {
            flags |= mask;
        }
        else {
            flags &= ~mask;
        }
    }

    private static bool HasFlag(int type) {
        var mask = 1 << type;

        return (flags & mask) != 0;
    }

    private static void GenerateGuaranteedLoot() {
        foreach (var loot in ModContent.GetContent<ModChestLoot>()) {
            var chests = new List<Chest>();

            for (var i = 0; i < Main.maxChests; i++) {
                var chest = Main.chest[i];

                if (chest == null) {
                    continue;
                }

                var tile = Framing.GetTileSafely(chest.x, chest.y);

                var validTile = tile.TileType == loot.TileType;
                var validTileFrame = false;

                foreach (var frame in loot.Frames) {
                    if (tile.TileFrameX == frame * 36) {
                        validTileFrame = true;
                        break;
                    }
                }

                if (!validTile || !validTileFrame) {
                    continue;
                }

                chests.Add(chest);
            }

            while (!HasFlag(loot.ItemType)) {
                var chest = WorldGen.genRand.Next(chests);
                var stack = loot.Stack.Value;

                if (chest.HasItem(loot.ItemType) || chest.TryAddItem(loot.ItemType, stack, loot.RandomSlot)) {
                    SetFlag(loot.ItemType, true);
                    break;
                }
            }
        }
    }

    private static void GenerateExtraLoot() {
        foreach (var loot in ModContent.GetContent<ModChestLoot>()) {
            for (var i = 0; i < Main.maxChests; i++) {
                var chest = Main.chest[i];

                if (chest == null) {
                    continue;
                }

                var tile = Framing.GetTileSafely(chest.x, chest.y);

                var validTile = tile.TileType == loot.TileType;
                var validTileFrame = false;

                foreach (var frame in loot.Frames) {
                    if (tile.TileFrameX == frame * 36) {
                        validTileFrame = true;
                        break;
                    }
                }

                if (!validTile || !validTileFrame) {
                    continue;
                }

                var shouldBeAdded = !chest.HasItem(loot.ItemType) && WorldGen.genRand.NextBool(loot.Chance);

                if (!shouldBeAdded) {
                    continue;
                }

                var stack = loot.Stack.Value;

                if (!chest.TryAddItem(loot.ItemType, stack, loot.RandomSlot)) {
                    continue;
                }

                SetFlag(loot.ItemType, true);
            }
        }
    }
}
