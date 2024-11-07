using System.Collections.Generic;
using EndlessEscapade.Utilities.Extensions;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class SailboatSystem : ModSystem
{
    public const string BROKEN_SAILBOAT_PASS_NAME = $"{nameof(EndlessEscapade)}:{nameof(SailboatSystem)}";

    /// <summary>
    ///     Whether the Sailboat is repaired or not.
    /// </summary>
    public bool Repaired { get; private set; }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
        base.ModifyWorldGenTasks(tasks, ref totalWeight);

        var index = tasks.FindIndex(static pass => pass.Name == "Final Cleanup");

        if (index == -1) {
            return;
        }

        tasks.Insert(index + 1, new PassLegacy(BROKEN_SAILBOAT_PASS_NAME, GenerateBrokenSailboat));
    }

    public override void ClearWorld() {
        base.ClearWorld();

        Repaired = false;
    }

    public override void SaveWorldData(TagCompound tag) {
        base.SaveWorldData(tag);

        tag["repaired"] = Repaired;
    }

    public override void LoadWorldData(TagCompound tag) {
        base.LoadWorldData(tag);

        if (!tag.TryGet<bool>("repaired", out var value)) {
            return;
        }

        Repaired = value;
    }

    private void GenerateBrokenSailboat(GenerationProgress progress, GameConfiguration configuration) {
        progress.Message = Mod.GetLocalizationValue("UI.Generation.BrokenSailboat");


    }
}
