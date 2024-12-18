using ReLogic.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

public abstract class ModAmbienceSound : ModType
{
    /// <summary>
    ///     The sound style used by this ambience sound.
    /// </summary>
    public abstract SoundStyle Sound { get; }

    /// <summary>
    ///     The chance denominator of this ambience sound playing for every tick.
    /// </summary>
    public virtual int Chance { get; } = 100;

    public SlotId Slot { get; internal set; }

    public abstract bool IsAmbienceActive(in AmbienceContext context);

    protected sealed override void Register()
        => ModTypeLookup<ModAmbienceSound>.Register(this);
}