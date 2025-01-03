using Microsoft.Extensions.Primitives;
using System;

namespace EndlessEscapade.Generators.Utilities;

internal ref struct StringSegmentSplitEnumerator
{
    private readonly char separator;
    private readonly StringSplitOptions splitOptions;
    private readonly StringSegment source;
    private StringSegment current;
    private int position;

    public StringSegmentSplitEnumerator(StringSegment source, char separator, StringSplitOptions splitOptions)
    {
        this.separator = separator;
        this.splitOptions = splitOptions;
        this.source = source;
        current = default;
        position = 0;
    }

    public readonly StringSegmentSplitEnumerator GetEnumerator() => this;
    public readonly StringSegment Current => current;

    public bool MoveNext()
    {
    retry:
        if (position == source.Length)
            return false;
        var remaining = source.AsSpan(position);
        var index = remaining.IndexOf(separator);
        if (index != -1)
        {
            current = source.Substring(position, index);
            position += index + 1;
        }
        else
        {
            current = source.Substring(position);
            position = source.Length;
        }
        if ((splitOptions & StringSplitOptions.RemoveEmptyEntries) != 0 && current.Length == 0)
            goto retry;
        return true;
    }
}
