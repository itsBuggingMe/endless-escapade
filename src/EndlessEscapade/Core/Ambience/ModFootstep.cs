using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

public abstract class ModFootstep : ModType
{
    public abstract SoundStyle Sound { get; }

    public abstract string Material { get; }

    protected sealed override void Register()
        => ModTypeLookup<ModFootstep>.Register(this);
}