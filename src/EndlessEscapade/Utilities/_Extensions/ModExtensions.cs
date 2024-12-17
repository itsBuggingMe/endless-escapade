using System.Runtime.CompilerServices;

namespace EndlessEscapade.Utilities;

/// <summary>
///     Provides <see cref="Mod" /> extension methods.
/// </summary>
public static class ModExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetLocalizationValue(this Mod mod, string key)
        => mod.GetLocalization(key).Value;
}