namespace EndlessEscapade.Core.ECS;

public sealed class GlobalComponentRegistry<T> where T : struct
{
    private T component;

    private bool hasComponent;

    public ref T Get()
    {
        return ref component;
    }

    public void Set(T value)
    {
        component = value;
    }
    
    public bool Remove()
    {
        if (!hasComponent)
        {
            return false;
        }

        component = default;
        hasComponent = false;

        return true;
    }

    public bool Has()
    {
        return hasComponent;
    }
}