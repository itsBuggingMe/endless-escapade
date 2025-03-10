using System.Runtime.CompilerServices;

namespace EndlessEscapade.Core;

internal static class ThrowHelper
{
    public static void Throw_ArgumentOutOfRange(string message, int index, [CallerArgumentExpression(nameof(index))] string? paramName = null)
    {
        throw new ArgumentOutOfRangeException(paramName, index, message);
    }

    public static void Throw_InvalidOperation(string message)
    {
        throw new InvalidOperationException(message);
    }
}
