using System.Runtime.CompilerServices;

namespace EndlessEscapade.Utilities;

public static class MathUtils
{
    public static int DivCeil(int left, int right) 
    {
        return ((left - 1) / right) + 1;
    }
}
