namespace EndlessEscapade.Core.Graphics;

public sealed class ForegroundRenderHook : RenderHook
{
    public override void Load(Mod mod)
    {
        base.Load(mod);
        
        On_Main.DrawProjectiles += Main_DrawProjectiles_Draw;
    }

    private void Main_DrawProjectiles_Draw(On_Main.orig_DrawProjectiles orig, Main self)
    {
        Draw();
        
        orig(self);
    }
}