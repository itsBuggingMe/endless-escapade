using Terraria.DataStructures;
using Terraria.GameContent;

namespace EndlessEscapade.Content.Gores;

public class ShellCasingGore : ModGore
{
    public override void SetStaticDefaults() 
    {
        ChildSafety.SafeGore[Type] = true;
    }

    public override void OnSpawn(Gore gore, IEntitySource source) 
    {
        gore.scale = 0.5f;
        gore.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
    }

    public override bool Update(Gore gore) 
    {
        if (gore.alpha >= 255) 
        {
            gore.active = false;
        }
        else
        {
            gore.alpha += 5;
        }

        return true;
    }
}