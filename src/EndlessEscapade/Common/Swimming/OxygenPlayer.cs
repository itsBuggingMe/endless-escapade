using MonoMod.Cil;

namespace EndlessEscapade.Common.Swimming;

public sealed class OxygenPlayer : ModPlayer
{
    private StatModifier breathCapacity = new();
    private StatModifier breathEfficiency = new();

    public override void Load()
    {
        base.Load();

        IL_Player.CheckDrowning += Player_CheckDrowning_Edit;
    }

    public ref StatModifier GetBreathCapacity()
    {
        return ref breathCapacity;
    }

    public ref StatModifier GetBreathEfficiency()
    {
        return ref breathEfficiency;
    }

    private void Player_CheckDrowning_Edit(ILContext il)
    {
        try
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext
                (
                    MoveType.After,
                    static i => i.MatchLdarg(0),
                    static i => i.MatchLdfld<Player>("breathCD"),
                    static i => i.MatchLdarg(0),
                    static i => i.MatchCallOrCallvirt<Player>("get_breathCDMax")
                ))
            {
                throw new Exception();
            }

            c.EmitDelegate((int breathCDMax) => breathEfficiency.ApplyTo(breathCDMax));

            if (!c.TryGotoNext(MoveType.After, static i => i.MatchLdarg(0), static i => i.MatchLdarg(0), static i => i.MatchLdfld<Player>("breath"), static i => i.MatchLdcI4(1)))
            {
                throw new Exception();
            }

            c.Remove();

            c.EmitStloc(0);
            c.EmitDelegate((int breath) => breathCapacity.ApplyTo(breath));
        }
        catch (Exception)
        {
            MonoModHooks.DumpIL(Mod, il);
        }
    }
}