using System.Collections.Generic;

namespace EndlessEscapade.Utilities;

/// <summary>
///     Provides <see cref="Chest" /> extension methods.
/// </summary>
public static class ChestExtensions
{
    /// <summary>
    ///     Checks whether a <see cref="Chest"/> has an <see cref="Item"/> of a specified type or not.
    /// </summary>
    /// <param name="chest">The <see cref="Chest"/> to check.</param>
    /// <param name="type">The <see cref="Item"/> type to check.</param>
    /// <returns><c>true</c> if the <see cref="Chest"/> has the <see cref="Item"/> of the specified type; otherwise, <c>false</c>.</returns>
    public static bool HasItem(this Chest chest, int type)
    {
        for (var i = 0; i < Chest.maxItems; i++)
        {
            var item = chest.item[i];

            if (!item.IsAir && item.type == type)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Attempts to insert an <see cref="Item"/> in a <see cref="Chest"/>.
    /// </summary>
    /// <param name="chest">The <see cref="Chest"/> to insert the <see cref="Item"/> into.</param>
    /// <param name="type">The <see cref="Item"/> type to insert.</param>
    /// <param name="stack">The <see cref="Item"/> stack to insert.</param>
    /// <param name="randomSlot">Whether the <see cref="Item"/> should be inserted into a random empty slot or not.</param>
    /// <returns><c>true</c> if the <see cref="Item"/> was successfully inserted; otherwise, <c>false</c>.</returns>
    public static bool TryInsertItem(this Chest chest, int type, int stack, bool randomSlot)
    {
        if (!chest.TryGetEmptySlot(out var index, randomSlot) || type == ItemID.None)
        {
            return false;
        }

        chest.item[index].SetDefaults(type);
        chest.item[index].stack = stack;

        return true;
    }

    /// <summary>
    ///     Attempts to retrieve an empty slot from a <see cref="Chest"/>.
    /// </summary>
    /// <param name="chest">The <see cref="Chest"/> to retrieve the slot from.</param>
    /// <param name="index"></param>
    /// <param name="randomSlot"></param>
    /// <returns><c>true</c> if an empty slot was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetEmptySlot(this Chest chest, out int index, bool randomSlot)
    {
        var indices = new List<int>();

        for (var i = 0; i < Chest.maxItems; i++)
        {
            var item = chest.item[i];

            if (item != null && item.IsAir)
            {
                indices.Add(i);
            }
        }

        if (indices.Count <= 0)
        {
            index = -1;

            return false;
        }

        index = randomSlot ? Main.rand.Next(indices) : indices[0];

        return true;
    }
}