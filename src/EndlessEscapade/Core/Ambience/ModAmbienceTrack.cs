using ReLogic.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

/// <summary>
/// 
/// </summary>
public abstract class ModAmbienceTrack : ModType
{
    /// <summary>
    ///     Gets the sound style of the ambience track.
    /// </summary>
    public abstract SoundStyle Sound { get; }

    /// <summary>
    ///     Gets the step value used for performing volume fade-ins.
    /// </summary>
    public virtual float StepIn { get; } = 0.05f;

    /// <summary>
    ///     Gets the step value used for performing volume fade-outs.
    /// </summary>
    public virtual float StepOut { get; } = 0.05f;

    /// <summary>
    ///     Gets the volume of the ambience track.
    /// </summary>
    public float Volume
    {
        get => volume;
        internal set => volume = MathHelper.Clamp(value, 0f, 1f);
    }

    /// <summary>
    ///     Gets the sound slot that points to the sound instance of the ambience track.
    /// </summary>
    public SlotId Slot { get; internal set; }

    private float volume;

    public abstract bool IsAmbienceActive(in AmbienceContext context);

    protected sealed override void Register()
    {
        ModTypeLookup<ModAmbienceTrack>.Register(this);
    }
}