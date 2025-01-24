using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

/// <summary>
///     
/// </summary>
public abstract class ModFootstep : ModType
{
    /// <summary>
    ///     Gets the sound style of the footstep.
    /// </summary>
    public abstract SoundStyle Sound { get; }

    /// <summary>
    ///     Gets the tile material associated with the footstep.
    /// </summary>
    public abstract string Material { get; }

    protected sealed override void Register()
    {
        ModTypeLookup<ModFootstep>.Register(this);
    }
}