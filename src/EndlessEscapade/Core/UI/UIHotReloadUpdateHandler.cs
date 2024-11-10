using System.Reflection.Metadata;
using EndlessEscapade.Core.UI;

[assembly: MetadataUpdateHandler(typeof(UIHotReloadUpdateHandler))]

namespace EndlessEscapade.Core.UI;

internal static class UIHotReloadUpdateHandler
{
    // This method is required for the handler to work, despite not doing anything.
#pragma warning disable IDE0060 // Remove unused parameter
    internal static void ClearCache(Type[]? updatedTypes) { }
#pragma warning restore IDE0060 // Remove unused parameter

    internal static void UpdateApplication(Type[]? updatedTypes) {
        Main.QueueMainThreadAction(
            () => {
                foreach (var type in updatedTypes) {
                    // TODO: Implement a UISystem.
                }
            }
        );
    }
}
