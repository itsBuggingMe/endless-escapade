using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace EndlessEscapade.Common.World;

public sealed class SailboatSystem : ModSystem
{
    /// <summary>
    ///     Whether the Sailboat is repaired or not.
    /// </summary>
    public bool Repaired { get; private set; }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
        base.ModifyWorldGenTasks(tasks, ref totalWeight);

        var index = tasks.FindIndex(pass => pass.Name == "Final Cleanup");

        if (index == -1) {
            return;
        }
    }

    public override void ClearWorld() {
        base.ClearWorld();

        Repaired = false;
    }
}
