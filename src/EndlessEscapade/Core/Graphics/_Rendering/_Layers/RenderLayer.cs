namespace EndlessEscapade.Core.Graphics;

public sealed class RenderLayer : IDisposable, IComparable<RenderLayer>
{
    public RenderLevel Level { get; }
    
    public int Id { get; }

    public string Name { get; }
    
    public bool IsPixellated { get; }
    
    public bool IsDisposed { get; private set; }
    
    public RenderTarget2D Buffer { get; private set; }
    
    internal RenderLayer(RenderLevel level, int id, string name, bool isPixellated)
    {
        Level = level;
        
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        
        Id = id;
        
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));
     
        Name = name;
        IsPixellated = isPixellated;

        var width = isPixellated ? Main.screenWidth / 2 : Main.screenWidth;
        var height = isPixellated ? Main.screenHeight / 2 : Main.screenHeight;
        
        Main.QueueMainThreadAction(() => Buffer = new RenderTarget2D(Main.graphics.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.Depth16));
    }

    public override string ToString()
    {
        return $"Layer: {Id} {Name}";
    }

    public int CompareTo(RenderLayer? other)
    {
        return other == null ? -1 : other.Id.CompareTo(Id);
    }

    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            Buffer?.Dispose();
        }
        
        Buffer = null;
        
        IsDisposed = true;
    }
}