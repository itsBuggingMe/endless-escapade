namespace EndlessEscapade.Core.Graphics;

public static class MeshRendering
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    /// <summary>
    ///     Draws a mesh.
    /// </summary>
    /// <param name="mesh">The mesh to draw.</param>
    /// <param name="effect">The effect to use.</param>
    public static void Draw(in Mesh mesh, Effect effect)
    {
        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            
            Device.DrawUserIndexedPrimitives(mesh.Type, mesh.Vertices, 0, mesh.Vertices.Length, mesh.Indices, 0, mesh.Count);
        }
    }
}