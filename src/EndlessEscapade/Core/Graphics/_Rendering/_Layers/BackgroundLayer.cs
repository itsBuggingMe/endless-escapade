namespace EndlessEscapade.Core.Graphics;

public sealed class BackgroundLayer : RenderLayer
{
    internal override void Subscribe()
    {
        base.Subscribe();
        
        On_Main.CheckMonoliths += On_MainOnCheckMonoliths;
        On_Main.DrawProjectiles += On_MainOnDrawProjectiles;
    }

    private void On_MainOnDrawProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
    {
        Render();
        
        orig(self);
    }

    private void On_MainOnCheckMonoliths(On_Main.orig_CheckMonoliths orig)
    {
        Fill();
        
        orig();
    }
}