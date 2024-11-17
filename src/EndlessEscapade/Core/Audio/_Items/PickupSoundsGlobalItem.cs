using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.Audio;
using Terraria.UI;

namespace EndlessEscapade.Core.Audio;

[Autoload(Side = ModSide.Client)]
public sealed class PickupSoundsGlobalItem : GlobalProjectile
{
    public override void Load() {
        base.Load();

        IL_ItemSlot.LeftClick_ItemArray_int_int += ItemSlot_LeftClick_Edit;
    }

    private void ItemSlot_LeftClick_Edit(ILContext il) {
        try {
            var c = new ILCursor(il);

            while (c.TryGotoNext(static i => i.MatchCallOrCallvirt(typeof(SoundEngine), nameof(SoundEngine.PlaySound)))) {
                var label = c.DefineLabel();

                c.Index -= 6; // Move to "ldc.i4.7"

                c.EmitBr(label);

                c.Index += 8; // Move to "pop"

                c.MarkLabel(label);

                c.EmitLdarg0(); // Push "Item[] inv"
                c.EmitLdarg2(); // Push "int slot"

                c.EmitDelegate(
                    static (Item[] inv, int slot) => {
                        SoundEngine.PlaySound(
                            new SoundStyle("EndlessEscapade/Assets/Sounds/Items/Pickups/Sword", 4) {
                                SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest,
                                PitchVariance = 0.2f,
                                Volume = 0.8f
                            }
                        );
                    }
                );
            }
        }
        catch (Exception) {
            MonoModHooks.DumpIL(Mod, il);
        }
    }
}
