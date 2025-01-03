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
///// <summary>
///// A struct similar to <see cref="ArraySegment{T}"/> but for strings.
///// </summary>
///// <remarks>WARNING <br/>
///// This struct uses binary comparision rules (ordinal) which differs from default on strings.<br/>
///// GetHashCode impl is also different, such that <c>"text".GetHashCode() != "text".AsSegment().GetHashCode()</c>
///// </remarks>
//[StructLayout(LayoutKind.Auto)]
//internal readonly struct StringSegment : IEquatable<StringSegment>, IComparable<StringSegment>
//{
//    public readonly string str;
//    public readonly int start, count;

//    public bool IsEmpty => count == 0;

//    public StringSegment(string str) : this(str, 0, str.Length) { }
//    public StringSegment(string str, int start) : this(str, start, str.Length - start) { }
//    public StringSegment(string str, int start, int count)
//    {
//        // just let a span validate and throw
//        _ = str.AsSpan(start, count);
//        this.str = str;
//        this.start = start;
//        this.count = count;
//    }

//    public readonly bool Equals(string other) => Equals(new StringSegment(other));
//    public readonly bool Equals(StringSegment other)
//    {
//        return AsSpan().Equals(other.AsSpan(), StringComparison.Ordinal);
//    }
//    public override bool Equals(object obj)
//    {
//        return obj is StringSegment segment && Equals(segment);
//    }
//    public override int GetHashCode()
//    {
//        return MemoryHelpers.GetHashCodeRaw(MemoryMarshal.Cast<char, byte>(AsSpan()));
//    }

//    public readonly ReadOnlySpan<char> AsSpan()
//        => str.AsSpan(start, count);
//    public readonly ReadOnlySpan<char> AsSpan(int start)
//        => str.AsSpan(this.start + start, count);
//    public readonly ReadOnlySpan<char> AsSpan(int start, int count)
//        => str.AsSpan(this.start + start, count);

//    public readonly ReadOnlyMemory<char> AsMemory()
//        => str.AsMemory(start, count);
//    public readonly ReadOnlyMemory<char> AsMemory(int start)
//        => str.AsMemory(this.start + start, count);
//    public readonly ReadOnlyMemory<char> AsMemory(int start, int count)
//        => str.AsMemory(this.start + start, count);

//    public StringSegment Slice(int start)
//        => new(str, this.start + start, count - start);
//    public StringSegment Slice(int start, int count)
//        => new(str, this.start + start, count);

//    public override string ToString()
//        => str.Substring(start, count);

//    public int CompareTo(StringSegment other)
//        => AsSpan().CompareTo(other.AsSpan(), StringComparison.Ordinal);

//    public static implicit operator StringSegment(string str) => new(str);
//    public static bool operator ==(StringSegment left, StringSegment right) => left.Equals(right);
//    public static bool operator !=(StringSegment left, StringSegment right) => !left.Equals(right);

//}