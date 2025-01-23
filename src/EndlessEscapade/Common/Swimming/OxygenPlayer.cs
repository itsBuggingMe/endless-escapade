using MonoMod.Cil;

namespace EndlessEscapade.Common.Swimming;

public sealed class OxygenPlayer : ModPlayer
{
    private StatModifier breathCapacity = new();
    private StatModifier breathEfficiency = new();

    public ref StatModifier GetBreathCapacity()
    {
        return ref breathCapacity;
    }

    public ref StatModifier GetBreathEfficiency()
    {
        return ref breathEfficiency;
    }
}