using System.Runtime.CompilerServices;

namespace EndlessEscapade.Utilities;

/// <summary>
///     Provides <see cref="Player" /> extension methods.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    ///     Checks whether the <see cref="Player"/> is underwater or not.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="Player"/> is underwater; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnderwater(this Player player, bool includeSlopes = false)
    {
        return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir, includeSlopes);
    }

    /// <summary>
    ///     Checks whether the <see cref="Player"/> is on the ground or not.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="Player"/> is on the ground; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsGrounded(this Player player)
    {
        return player.velocity.Y == 0f;
    }
    
    /// <summary>
    ///     Checks whether the <see cref="Player"/> is mounted or not.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="Player"/> is mounted; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMounted(this Player player)
    {
        return player.mount.Active;
    }

    /// <summary>
    ///     Checks whether the <see cref="Player"/> is at surface level or not.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="Player"/> is at surface level; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool InSurface(this Player player)
    {
        return player.ZoneOverworldHeight && !player.ZoneUndergroundDesert;
    }
    
    /// <summary>
    ///     Checks whether the <see cref="Player"/> was on the ground or not.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="Player"/> was on the ground; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool WasGrounded(this Player player)
    {
        return player.oldVelocity.Y == 0f;
    }
}