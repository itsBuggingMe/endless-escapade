using MonoMod.Cil;

namespace EndlessEscapade.Common.Swimming;

internal sealed class OxygenHooks : ILoadable
{
    void ILoadable.Load(Mod mod)
    {
        IL_Player.CheckDrowning += Player_CheckDrowning_ModifyOxygen;
    }

    void ILoadable.Unload() { }

    private static void Player_CheckDrowning_ModifyOxygen(ILContext il)
    {
        try
        {
            var c = new ILCursor(il);
            
            c.TryGotoNext
            (
                MoveType.After,
                i => i.MatchLdarg(0),
                i => i.MatchLdfld<Player>("breathCD"),
                i => i.MatchLdarg(0),
                i => i.MatchCallOrCallvirt<Player>("get_breathCDMax")
            );
            
            c.EmitLdarg0();
            c.EmitDelegate
            (
                (int breathCDMax, Player p) =>
                {
                    breathCDMax = 10;
                    return breathCDMax;
                }
            );
            
            c.TryGotoNext
            (
                MoveType.After,
                i => i.MatchLdarg(0),
                i => i.MatchLdarg(0),
                i => i.MatchLdfld<Player>("breath"),
                i => i.MatchLdcI4(1)
            );
            
            c.Remove();
            
            c.EmitStloc(0);
            c.EmitLdarg0();

            c.EmitDelegate
            (
                (int breath, Player p) =>
                {
                    breath = 100;
                    return breath;
                }
            );
        }
        catch (Exception)
        {
            MonoModHooks.DumpIL(EndlessEscapade.Instance, il);
        }
    }
}