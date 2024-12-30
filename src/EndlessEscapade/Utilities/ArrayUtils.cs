namespace EndlessEscapade.Utilities;

public static class ArrayUtils
{
    public static void EnsureCapacity<T>(ref T[] array, int capacity)
    {
        var newCapacity = Math.Max(1, array.Length);

        while (newCapacity <= capacity)
        {
            newCapacity *= 2;
        }

        Array.Resize(ref array, newCapacity);
    }
}