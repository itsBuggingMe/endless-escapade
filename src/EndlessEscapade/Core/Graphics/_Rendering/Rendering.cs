namespace EndlessEscapade.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class Rendering : ILoadable
{
    /// <summary>
    /// 
    /// </summary>
    public const int MAX_VERTEX_COUNT = 65536;

    /// <summary>
    /// 
    /// </summary>
    public const int MAX_INDEX_COUNT = 65536;
    
    /// <summary>
    /// 
    /// </summary>
    public static VertexBuffer VertexBuffer { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public static IndexBuffer IndexBuffer { get; private set; }

    private static GraphicsDevice Device { get; } = Main.graphics.GraphicsDevice;

    void ILoadable.Load(Mod mod)
    {
        Main.QueueMainThreadAction
        (
            static () =>
            {
                VertexBuffer = new VertexBuffer(Device, typeof(VertexPositionColorTexture), MAX_VERTEX_COUNT, BufferUsage.WriteOnly);
                IndexBuffer = new IndexBuffer(Device, IndexElementSize.SixteenBits, MAX_INDEX_COUNT, BufferUsage.WriteOnly);
            }
        );
    }

    void ILoadable.Unload()
    {
        Main.QueueMainThreadAction
        (
            static () =>
            {
                VertexBuffer?.Dispose();
                VertexBuffer = null;
                
                IndexBuffer?.Dispose();
                IndexBuffer = null;
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sprite"></param>
    public static void Draw(in RenderContext context, in Sprite sprite) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mesh"></param>
    public static void Draw(in RenderContext context, in Mesh mesh) { }
}