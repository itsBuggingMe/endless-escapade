using MonoMod.Cil;

namespace EndlessEscapade.Common.Players
{
    public class OxygenChanges : ModSystem
    {
        public override void Load()
        {
            IL_Player.CheckDrowning += Player_CheckDrowning_Edit;
        }

        public override void Unload()
        {
            IL_Player.CheckDrowning -= Player_CheckDrowning_Edit;
        }
        private void Player_CheckDrowning_Edit(MonoMod.Cil.ILContext il)
        {
            var c = new ILCursor(il);
            c.TryGotoNext(MoveType.After,
                i => i.MatchLdarg(0),
                i => i.MatchLdfld<Player>("breathCD"),
                i => i.MatchLdarg(0),
                i => i.MatchCallOrCallvirt<Player>("get_breathCDMax"));
            c.EmitLdarg0();
            c.EmitDelegate((int breathCDMax, Player p) => // edit how long it takes between each breath stat update here
            {
                breathCDMax = 10;
                return breathCDMax;
            });
            c.TryGotoNext(MoveType.After,
                i => i.MatchLdarg(0),
                i => i.MatchLdarg(0),
                i => i.MatchLdfld<Player>("breath"),
                i => i.MatchLdcI4(1));
            c.Remove();
            c.EmitStloc(0);
            c.EmitLdarg0();
            c.EmitDelegate((int breath, Player p) => // edit how breath stat changes here
            {
                breath = 100;
                return breath;
            });
        }
    }
}