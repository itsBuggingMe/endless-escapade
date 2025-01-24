namespace EndlessEscapade.Framework.Graphics;

public sealed class BackgroundRenderHook : RenderHook
{
    public override void Load(Mod mod)
    {
        base.Load(mod);
        
        On_Main.DoDraw_DrawNPCsBehindTiles += Main_DoDraw_DrawNPCsBehindTiles_Draw;
    }

    private void Main_DoDraw_DrawNPCsBehindTiles_Draw(On_Main.orig_DoDraw_DrawNPCsBehindTiles orig, Main self)
    {
        Draw();
        
        orig(self);
    }
}