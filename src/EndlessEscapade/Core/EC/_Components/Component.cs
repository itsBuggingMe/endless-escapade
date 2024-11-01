namespace EndlessEscapade.Core.EC;

public abstract class Component
{
    public Entity Entity { get; set; }

    public virtual void Update() { }

    public virtual void Render() { }
}