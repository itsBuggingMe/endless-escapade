using EndlessEscapade.Core.Collections;

namespace EndlessEscapade.Tests;

public class SparseSetTests
{
    [Test]
    public void Enumerate_ReturnsData()
    {
        var set = new SparseSet<int>(10);
        
        set.Add(0, 1);
        set.Add(1, 2);
        set.Add(2, 3);

        set.Remove(0);

        CollectionAssert.AreEqual(new[] { 2, 3 }, set.ToArray());
    }
    
    [Test]
    public void Add_AddsElements()
    {
        var set = new SparseSet<int>(2);

        set.Add(0, 1);
        set.Add(1, 2);

        Assert.IsTrue(set.Has(0));
        Assert.IsTrue(set.Has(1));
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
    }
    
    [Test]
    public void Remove_RemovesElements()
    {
        var set = new SparseSet<int>(2);

        set.Add(0, 1);
        set.Add(1, 2);
        
        set.Remove(0);

        Assert.IsFalse(set.Has(0));
        Assert.IsTrue(set.Has(1));
    }

    [Test]
    public void Get_GetsElements()
    {
        var set = new SparseSet<int>(2);
        
        set.Add(0, 1);
        set.Add(1, 2);
        
        Assert.AreEqual(set.Get(0), 1);
        Assert.AreEqual(set.Get(1), 2);
    }

    [Test]
    public void Get_ResizesCapacity()
    {
        var set = new SparseSet<int>(1);
        
        set.Add(0, 1);
        set.Add(1, 2);
        
        Assert.AreEqual(2, set.Capacity);
        
        Assert.IsTrue(set.Has(0));
        Assert.IsTrue(set.Has(1));
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
    }
    
    [Test]
    public void TryGet_ReturnsCorrectValue()
    {
        var set = new SparseSet<int>(1);

        set.Add(0, 1);

        Assert.IsTrue(set.TryGet(0, out var value));
        Assert.AreEqual(1, value);

        set.Remove(0);
        
        Assert.IsFalse(set.TryGet(0, out value));
    }
    
    [Test]
    public void Clear_ClearsAllData()
    {
        var set = new SparseSet<int>(2);

        set.Add(0, 1);
        set.Add(1, 2);
        
        set.Clear();

        Assert.AreEqual(0, set.Count);
        
        Assert.IsFalse(set.Has(0));
        Assert.IsFalse(set.Has(1));
    }
    
    [Test]
    public void Resize_GrowsCapacity()
    {
        var set = new SparseSet<int>(2);

        set.Add(0, 1);
        set.Add(1, 2);
        
        set.Resize(4);

        set.Add(2, 3);
        set.Add(3, 4);

        Assert.AreEqual(4, set.Capacity);
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
        Assert.AreEqual(3, set.Get(2));
        Assert.AreEqual(4, set.Get(3));
    }
    
    [Test]
    public void Resize_ShrinksCapacity()
    {
        var set = new SparseSet<int>(4);

        set.Add(0, 1);
        set.Add(1, 2);
        set.Add(2, 3);
        set.Add(3, 4);

        set.Resize(2);
        
        Assert.AreEqual(2, set.Capacity);
        
        Assert.AreEqual(1, set.Get(0));
        Assert.AreEqual(2, set.Get(1));
        
        Assert.IsFalse(set.Has(2));
        Assert.IsFalse(set.Has(3));
    }
}