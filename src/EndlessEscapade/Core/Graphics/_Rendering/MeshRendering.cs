using EndlessEscapade.Utilities;

namespace EndlessEscapade.Core.Graphics;

public static class MeshRendering
{
    private static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    private static SpriteBatch SpriteBatch => Main.spriteBatch;
    
    /// <summary>
    ///     Draws a mesh to the screen.
    /// </summary>
    /// <param name="mesh">The mesh to draw.</param>
    /// <param name="parameters">The sprite batch parameters to use.</param>
    public static void Draw(in Mesh mesh, in SpriteBatchParameters parameters)
    {
        foreach (var pass in parameters.Effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            
            Device.DrawUserIndexedPrimitives(mesh.Type, mesh.Vertices, 0, mesh.Vertices.Length, mesh.Indices, 0, mesh.Count);
        }
    }
}