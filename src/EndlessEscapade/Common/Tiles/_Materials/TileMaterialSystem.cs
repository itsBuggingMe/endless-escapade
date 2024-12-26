using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EndlessEscapade.Common.Tiles;

/// <summary>
///     Handles registration and loading of <see cref="Tile"/> materials through <see cref="TileMaterialAttribute" />
///     for modded entries, and manual callbacks for vanilla entries.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class TileMaterialSystem : ModSystem
{
    private static readonly Dictionary<int, string> Materials = [];

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        LoadModdedMaterials();
        LoadVanillaMaterials();
    }

    /// <summary>
    ///     Registers a <see cref="Tile"/> material from a type set.
    /// </summary>
    /// <param name="material">The name of the material.</param>
    /// <param name="types">The <see cref="Tile"/> types associated with the material.</param>
    public static void RegisterMaterial(string material, params int[] types)
    {
        for (var i = 0; i < types.Length; i++)
        {
            Materials[types[i]] = material;
        }
    }

    /// <summary>
    ///     Registers a <see cref="Tile"/> material from a content set.
    /// </summary>
    /// <param name="material">The name of the material.</param>
    /// <param name="set">The set associated with the material.</param>
    public static void RegisterMaterial(string material, bool[] set)
    {
        for (var i = 0; i < set.Length; i++)
        {
            if (set[i])
            {
                RegisterMaterial(material, i);
            }
        }
    }

    /// <summary>
    ///     Attempts to retrieve a material from a <see cref="Tile"/>'s type.
    /// </summary>
    /// <param name="type">The type of the <see cref="Tile"/>.</param>
    /// <param name="material">The name of the material retrieved.</param>
    /// <returns><c>true</c> if a material was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetMaterial(int type, [MaybeNullWhen(false)] out string material)
    {
        return Materials.TryGetValue(type, out material);
    }

    /// <summary>
    ///     Attempts to retrieve a material from a <see cref="Tile"/>.
    /// </summary>
    /// <param name="tileType">The type of the <see cref="Tile"/>.</param>
    /// <param name="material">The name of the material retrieved.</param>
    /// <returns><c>true</c> if a material was successfully retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetMaterial(Tile tile, out string material)
    {
        return TryGetMaterial(tile.TileType, out material);
    }

    private static void LoadModdedMaterials()
    {
        foreach (var tile in ModContent.GetContent<ModTile>())
        {
            var type = tile.GetType();

            var attribute = type.GetCustomAttribute<TileMaterialAttribute>();

            if (attribute == null)
            {
                continue;
            }

            Materials[tile.Type] = attribute.Name;
        }
    }

    private static void LoadVanillaMaterials()
    {
        RegisterMaterial
        (
            "Grass",
            TileID.LivingMahoganyLeaves
        );

        RegisterMaterial("Grass", TileID.Sets.Grass);

        RegisterMaterial
        (
            "Stone",
            TileID.GrayBrick,
            TileID.StoneSlab,
            TileID.Mudstone
        );

        RegisterMaterial("Stone", TileID.Sets.Stone);

        RegisterMaterial
        (
            "Wood",
            TileID.Platforms,
            TileID.WoodBlock,
            TileID.AshWood,
            TileID.Shadewood,
            TileID.Pearlwood,
            TileID.BorealWood,
            TileID.LivingWood,
            TileID.DynastyWood,
            TileID.Ebonwood,
            TileID.SpookyWood,
            TileID.LivingWood,
            TileID.RichMahogany,
            TileID.PalmWood
        );

        RegisterMaterial
        (
            "Sand",
            TileID.Sand,
            TileID.Crimsand,
            TileID.Ebonsand,
            TileID.Pearlsand
        );
    }
}