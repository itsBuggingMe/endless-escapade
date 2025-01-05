namespace EndlessEscapade.Core.Graphics;

public readonly struct RenderLayer
{
    public int Id { get; }

    public string Name { get; }
    
    internal RenderLayer(int id, string name)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        
        Id = id;
        
        ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));
     
        Name = name;
    }

    public override string ToString()
    {
        return $"Layer: {Id} {Name}";
    }
}