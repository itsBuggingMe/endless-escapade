using ReLogic.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

public abstract class ModAmbienceSound : ModType
{
    /// <summary>
    ///     Gets the sound style of the ambience sound.
    /// </summary>
    public abstract SoundStyle Sound { get; }

    /// <summary>
    ///     Gets the chance denominator of the ambience sound playing for every tick.
    /// </summary>
    public virtual int Chance { get; } = 100;

    /// <summary>
    ///     Gets the sound slot that points to the sound instance of the ambience sound.
    /// </summary>
    public SlotId Slot { get; internal set; }

    public abstract bool IsAmbienceActive(in AmbienceContext context);

    protected sealed override void Register()
    {
        ModTypeLookup<ModAmbienceSound>.Register(this);
    }
}