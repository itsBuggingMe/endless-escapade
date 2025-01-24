using EndlessEscapade.Framework.Collections;
using NUnit.Framework;

namespace EndlessEscapade.Framework.Tests.Collections;

public sealed class SparseSetTests
{
    [Test]
    public void Enumerate_ReturnsElements()
    {
        var set = new SparseSet<int>(10);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);
        Assert.That(set.Add(2, 3), Is.True);

        Assert.That(set.Remove(0), Is.True);

        Assert.That(set, Is.EquivalentTo(new[] { 2, 3 }));
    }

    [Test]
    public void Add_AddsElements()
    {
        var set = new SparseSet<int>(2);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        Assert.That(set.Has(0), Is.True);
        Assert.That(set.Has(1), Is.True);

        Assert.That(set.Get(0), Is.EqualTo(1));
        Assert.That(set.Get(1), Is.EqualTo(2));
    }

    [Test]
    public void Add_GrowsCapacity()
    {
        var set = new SparseSet<int>(1);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        Assert.That(set.Capacity, Is.EqualTo(4));

        Assert.That(set.Has(0), Is.True);
        Assert.That(set.Has(1), Is.True);

        Assert.That(set.Get(0), Is.EqualTo(1));
        Assert.That(set.Get(1), Is.EqualTo(2));
    }

    [Test]
    public void Remove_RemovesElements()
    {
        var set = new SparseSet<int>(2);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        Assert.That(set.Remove(0), Is.True);

        Assert.That(set.Has(0), Is.False);
        Assert.That(set.Has(1), Is.True);
    }

    [Test]
    public void Get_GetsElements()
    {
        var set = new SparseSet<int>(2);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        Assert.That(set.Get(0), Is.EqualTo(1));
        Assert.That(set.Get(1), Is.EqualTo(2));
    }

    [Test]
    public void TryGet_ReturnsElements()
    {
        var set = new SparseSet<int>(1);

        Assert.That(set.Add(0, 1), Is.True);

        Assert.That(set.TryGet(0, out var value), Is.True);
        Assert.That(value, Is.EqualTo(1));

        Assert.That(set.Remove(0), Is.True);

        Assert.That(set.TryGet(0, out value), Is.False);
    }

    [Test]
    public void Clear_ClearsElements()
    {
        var set = new SparseSet<int>(2);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        set.Clear();

        Assert.That(set.Count, Is.EqualTo(0));

        Assert.That(set.Has(0), Is.False);
        Assert.That(set.Has(1), Is.False);
    }

    [Test]
    public void Resize_ShrinksCapacity()
    {
        var set = new SparseSet<int>(4);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);
        Assert.That(set.Add(2, 3), Is.True);
        Assert.That(set.Add(3, 4), Is.True);

        set.Resize(2);

        Assert.That(set.Capacity, Is.EqualTo(2));

        Assert.That(set.Get(0), Is.EqualTo(1));
        Assert.That(set.Get(1), Is.EqualTo(2));

        Assert.That(set.Has(2), Is.False);
        Assert.That(set.Has(3), Is.False);
    }

    [Test]
    public void Set()
    {
        var set = new SparseSet<int>(1);

        Assert.That(set.Add(0, 1), Is.True);

        set.Set(0, 2);

        Assert.That(set.Has(0), Is.True);

        Assert.That(set.Get(0), Is.EqualTo(2));
    }

    [Test]
    public void Resize_GrowsCapacity()
    {
        var set = new SparseSet<int>(2);

        Assert.That(set.Add(0, 1), Is.True);
        Assert.That(set.Add(1, 2), Is.True);

        set.Resize(4);

        Assert.That(set.Add(2, 3), Is.True);
        Assert.That(set.Add(3, 4), Is.True);

        Assert.That(set.Capacity, Is.EqualTo(4));

        Assert.That(set.Get(0), Is.EqualTo(1));
        Assert.That(set.Get(1), Is.EqualTo(2));
        Assert.That(set.Get(2), Is.EqualTo(3));
        Assert.That(set.Get(3), Is.EqualTo(4));
    }

    [Test]
    public void EnsureCapacity_GrowsCapacity()
    {
        var set = new SparseSet<int>(2);

        set.EnsureCapacity(4);

        Assert.That(set.Capacity, Is.EqualTo(8));
    }
}
