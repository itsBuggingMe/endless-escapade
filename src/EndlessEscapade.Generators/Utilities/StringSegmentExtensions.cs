using Microsoft.Extensions.Primitives;
using System;

namespace EndlessEscapade.Generators.Utilities;

internal static class StringSegmentExtensions
{
    public static StringSegment AsSegment(this string str) => new(str);
    public static StringSegment AsSegment(this string str, int start) => new(str, start, str.Length - start);
    public static StringSegment AsSegment(this string str, int start, int count) => new(str, start, count);

    /// <summary>
    /// Alloc-free alternative to string.Split that only enumerates the substrings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitChar"></param>
    /// <returns></returns>
    public static unsafe StringSegmentSplitEnumerator SplitEx(this string str, char splitChar, StringSplitOptions options = StringSplitOptions.None)
    {
        return new(str, splitChar, options);
    }
    /// <summary>
    /// Alloc-free alternative to string.Split that only enumerates the substrings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitChar"></param>
    /// <returns></returns>
    public static unsafe StringSegmentSplitEnumerator SplitEx(this StringSegment str, char splitChar, StringSplitOptions options = StringSplitOptions.None)
    {
        return new(str, splitChar, options);
    }
}
