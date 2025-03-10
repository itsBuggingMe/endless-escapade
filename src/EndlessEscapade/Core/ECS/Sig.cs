using System.Collections.Generic;
using System.Diagnostics;

namespace EndlessEscapade.Core.ECS;
//todo: agg inline all
public struct Rec<T> : IRec
{
    private static readonly ArchetypeID CachedArchetypeID = Archetype.GetArchetypeID<Rec<T>>();
    public readonly ArchetypeID ArchetypeID => CachedArchetypeID;

    public T Item1;

    public T1 GetAt<T1>(int index)
    {
        if (index != 0)
            throw new IndexOutOfRangeException();
        return (T1)(object)Item1!;
    }

    public void AppendTypes(List<Type> appendTo) => appendTo.Add(typeof(T));
    public void SetArchetype(Archetype archetype, int index) => archetype.GetComponent<T>(index) = Item1;

    public static implicit operator Rec<T>(T val) => new() { Item1 = val };
}

public struct Rec<T1, T2> : IRec
{
    private static readonly ArchetypeID CachedArchetypeID = Archetype.GetArchetypeID<Rec<T1, T2>>();
    public readonly ArchetypeID ArchetypeID => CachedArchetypeID;

    public T1 Item1;
    public T2 Item2;

    public T GetAt<T>(int index)
    {
        return index switch
        {
            0 => (T)(object)Item1!,
            1 => (T)(object)Item2!,
            _ => throw new IndexOutOfRangeException()
        };
    }

    public void AppendTypes(List<Type> appendTo)
    {
        appendTo.Add(typeof(T1));
        appendTo.Add(typeof(T2));
    }

    public void SetArchetype(Archetype archetype, int index)
    {
        archetype.GetComponent<T1>(index) = Item1;
        archetype.GetComponent<T2>(index) = Item2;
    }

    public static implicit operator Rec<T1, T2>((T1, T2) val) => new() { Item1 = val.Item1, Item2 = val.Item2 };
}

public struct Rec<T1, T2, T3> : IRec
{
    private static readonly ArchetypeID CachedArchetypeID = Archetype.GetArchetypeID<Rec<T1, T2, T3>>();
    public readonly ArchetypeID ArchetypeID => CachedArchetypeID;

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;

    public T GetAt<T>(int index)
    {
        return index switch
        {
            0 => (T)(object)Item1!,
            1 => (T)(object)Item2!,
            2 => (T)(object)Item3!,
            _ => throw new IndexOutOfRangeException()
        };
    }

    public void AppendTypes(List<Type> appendTo)
    {
        appendTo.Add(typeof(T1));
        appendTo.Add(typeof(T2));
        appendTo.Add(typeof(T3));
    }

    public void SetArchetype(Archetype archetype, int index)
    {
        archetype.GetComponent<T1>(index) = Item1;
        archetype.GetComponent<T2>(index) = Item2;
        archetype.GetComponent<T3>(index) = Item3;
    }

    public static implicit operator Rec<T1, T2, T3>((T1, T2, T3) val) => new() { Item1 = val.Item1, Item2 = val.Item2, Item3 = val.Item3 };
}

public struct Rec<T1, T2, T3, T4> : IRec
{
    private static readonly ArchetypeID CachedArchetypeID = Archetype.GetArchetypeID<Rec<T1, T2, T3, T4>>();
    public readonly ArchetypeID ArchetypeID => CachedArchetypeID;

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;

    public T GetAt<T>(int index)
    {
        return index switch
        {
            0 => (T)(object)Item1!,
            1 => (T)(object)Item2!,
            2 => (T)(object)Item3!,
            3 => (T)(object)Item4!,
            _ => throw new IndexOutOfRangeException()
        };
    }

    public void AppendTypes(List<Type> appendTo)
    {
        appendTo.Add(typeof(T1));
        appendTo.Add(typeof(T2));
        appendTo.Add(typeof(T3));
        appendTo.Add(typeof(T4));
    }

    public void SetArchetype(Archetype archetype, int index)
    {
        archetype.GetComponent<T1>(index) = Item1;
        archetype.GetComponent<T2>(index) = Item2;
        archetype.GetComponent<T3>(index) = Item3;
        archetype.GetComponent<T4>(index) = Item4;
    }

    public static implicit operator Rec<T1, T2, T3, T4>((T1, T2, T3, T4) val) => new() { Item1 = val.Item1, Item2 = val.Item2, Item3 = val.Item3, Item4 = val.Item4 };
}

public struct Rec<T1, T2, T3, T4, TRest> : IRec where TRest : IRec
{
    private static readonly ArchetypeID CachedArchetypeID = Archetype.GetArchetypeID<Rec<T1, T2, T3, T4, TRest>>();
    public readonly ArchetypeID ArchetypeID => CachedArchetypeID;

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public TRest Rest;

    public T GetAt<T>(int index)
    {
        return index switch
        {
            0 => (T)(object)Item1!,
            1 => (T)(object)Item2!,
            2 => (T)(object)Item3!,
            3 => (T)(object)Item4!,
            _ => Rest.GetAt<T>(index - 4)
        };
    }

    public void AppendTypes(List<Type> appendTo)
    {
        appendTo.Add(typeof(T1));
        appendTo.Add(typeof(T2));
        appendTo.Add(typeof(T3));
        appendTo.Add(typeof(T4));
        Rest.AppendTypes(appendTo);
    }

    public void SetArchetype(Archetype archetype, int index)
    {
        archetype.GetComponent<T1>(index) = Item1;
        archetype.GetComponent<T2>(index) = Item2;
        archetype.GetComponent<T3>(index) = Item3;
        archetype.GetComponent<T4>(index) = Item4;

        Rest.SetArchetype(archetype, index);
    }
}


public interface IRec
{
    ArchetypeID ArchetypeID { get; }
    public T GetAt<T>(int index);
    public void AppendTypes(List<Type> appendTo);
    public void SetArchetype(Archetype archetype, int index);
    [ThreadStatic]
    public static readonly List<Type> SharedTypeList = [];
}