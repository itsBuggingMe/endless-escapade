namespace EndlessEscapade.Framework;

public ref struct EntityEnumerator
{
    public ref Entity Current => ref current;

    private ref Entity current;

    public EntityEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        return false;
    }
}