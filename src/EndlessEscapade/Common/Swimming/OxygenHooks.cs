using MonoMod.Cil;

namespace EndlessEscapade.Common.Swimming;

file sealed class OxygenHooks : ILoadable
{
    void ILoadable.Load(Mod mod)
    {
        IL_Player.CheckDrowning += Player_CheckDrowning_Edit;
    }
    
    void ILoadable.Unload() { }
    
    private static void Player_CheckDrowning_Edit(ILContext il)
    {
        try
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.After, static i => i.MatchLdarg0(), static i => i.MatchLdfld<Player>("breathCD"), static i => i.MatchLdarg0(), static i => i.MatchCallOrCallvirt<Player>("get_breathCDMax")))
            {
                throw new Exception();
            }

            c.EmitLdarg0();
            
            c.EmitDelegate
            (
                static (Player player, int breathCDMax) =>
                {
                    if (!player.TryGetModPlayer(out OxygenPlayer oxygenPlayer))
                    {
                        return;
                    }
                    
                    oxygenPlayer.GetBreathEfficiency().ApplyTo(breathCDMax);
                }
            );

            if (!c.TryGotoNext(MoveType.After, static i => i.MatchLdarg0(), static i => i.MatchLdarg0(), static i => i.MatchLdfld<Player>("breath"), static i => i.MatchLdcI4(1)))
            {
                throw new Exception();
            }

            c.Remove();

            c.EmitLdarg0();
            c.EmitStloc0();
            
            c.EmitDelegate
            (
                static (Player player, int breath) =>
                {
                    if (!player.TryGetModPlayer(out OxygenPlayer oxygenPlayer))
                    {
                        return;
                    }
                    
                    oxygenPlayer.GetBreathCapacity().ApplyTo(breath);
                }
            );
        }
        catch (Exception)
        {
            MonoModHooks.DumpIL(EndlessEscapade.Instance, il);
        }
    }
}