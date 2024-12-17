using ReLogic.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

public abstract class ModAmbienceTrack : ModType
{
    private float volume;

    /// <summary>
    ///     The sound style used by this ambience track.
    /// </summary>
    public abstract SoundStyle Sound { get; }

    /// <summary>
    ///     The step value used for performing volume fade-ins.
    /// </summary>
    public virtual float StepIn { get; } = 0.05f;

    /// <summary>
    ///     The step value used for performing volume fade-outs.
    /// </summary>
    public virtual float StepOut { get; } = 0.05f;

    /// <summary>
    ///     The current volume of this ambience track.
    /// </summary>
    public float Volume
    {
        get => volume;
        internal set => volume = MathHelper.Clamp(value, 0f, 1f);
    }

    /// <summary>
    ///     The sound slot that points to the sound instance of this ambience track.
    /// </summary>
    public SlotId Slot { get; internal set; }

    public abstract bool IsAmbienceActive(in AmbienceContext context);

    protected sealed override void Register()
    {
        ModTypeLookup<ModAmbienceTrack>.Register(this);
    }
}