using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Tests;

public sealed class SparseSetTests
{
    [Test]
    public void Enumerate_ReturnsElements()
    {
        var set = new SparseSet<int>(10);
        
        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        Assert.IsTrue(set.Add(2, 3));

        Assert.IsTrue(set.Remove(0));

        CollectionAssert.AreEqual(new [] { 2, 3 }, set);
    }
    
    [Test]
    public void Add_AddsElements()
    {
        var set = new SparseSet<int>(2);

        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));

        Assert.IsTrue(set.Has(0));
        Assert.IsTrue(set.Has(1));
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
    }
    
    [Test]
    public void Add_GrowsCapacity()
    {
        var set = new SparseSet<int>(1);
        
        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        
        Assert.AreEqual(2, set.Capacity);
        
        Assert.IsTrue(set.Has(0));
        Assert.IsTrue(set.Has(1));
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
    }
    
    [Test]
    public void Remove_RemovesElements()
    {
        var set = new SparseSet<int>(2);

        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        
        Assert.IsTrue(set.Remove(0));

        Assert.IsFalse(set.Has(0));
        Assert.IsTrue(set.Has(1));
    }

    [Test]
    public void Get_GetsElements()
    {
        var set = new SparseSet<int>(2);
        
        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        
        Assert.AreEqual(set.Get(0), 1);
        Assert.AreEqual(set.Get(1), 2);
    }
    
    [Test]
    public void TryGet_ReturnsElements()
    {
        var set = new SparseSet<int>(1);

        Assert.IsTrue(set.Add(0, 1));

        Assert.IsTrue(set.TryGet(0, out var value));
        Assert.AreEqual(1, value);

        Assert.IsTrue(set.Remove(0));
        
        Assert.IsFalse(set.TryGet(0, out value));
    }
    
    [Test]
    public void Clear_ClearsElements()
    {
        var set = new SparseSet<int>(2);
        
        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        
        set.Clear();

        Assert.AreEqual(0, set.Count);
        
        Assert.IsFalse(set.Has(0));
        Assert.IsFalse(set.Has(1));
    }
    
    [Test]
    public void Resize_ShrinksCapacity()
    {
        var set = new SparseSet<int>(4);

        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        Assert.IsTrue(set.Add(2, 3));
        Assert.IsTrue(set.Add(3, 4));

        set.Resize(2);
        
        Assert.AreEqual(2, set.Capacity);
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
        
        Assert.IsFalse(set.Has(2));
        Assert.IsFalse(set.Has(3));
    }

    [Test]
    public void Set()
    {
        var set = new SparseSet<int>(1);
        
        Assert.IsTrue(set.Add(0, 1));
        
        set.Set(0, 2);
        
        Assert.IsTrue(set.Has(0));
        
        Assert.AreEqual(2, set.Get(0));
    }
    
    [Test]
    public void Resize_GrowsCapacity()
    {
        var set = new SparseSet<int>(2);

        Assert.IsTrue(set.Add(0, 1));
        Assert.IsTrue(set.Add(1, 2));
        
        set.Resize(4);

        Assert.IsTrue(set.Add(2, 3));
        Assert.IsTrue(set.Add(3, 4));

        Assert.AreEqual(4, set.Capacity);
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
        Assert.AreEqual(3, set.Get(2));
        Assert.AreEqual(4, set.Get(3));
    }

    [Test]
    public void EnsureCapacity_GrowsCapacity()
    {
        var set = new SparseSet<int>(2);
        
        set.EnsureCapacity(4);
        
        Assert.AreEqual(8, set.Capacity);
    }
}