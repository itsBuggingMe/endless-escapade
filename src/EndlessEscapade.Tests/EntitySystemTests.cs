using EndlessEscapade.Core.ECS;

namespace EndlessEscapade.Tests;

public sealed class EntityRegistryTests
{
    [Test]
    public void Create_CreatesEntities()
    {
        var registry = new EntityRegistry(1);

        registry.Create();
        
        Assert.IsTrue(registry.Has(0));
    }

    [Test]
    public void Destroy_DestroysEntities()
    {
        var registry = new EntityRegistry(1);

        registry.Create();
        
        Assert.IsTrue(registry.Destroy(0));
    }

    [Test]
    public void Has_ChecksEntities()
    {
        var registry = new EntityRegistry(1);

        registry.Create();
        
        Assert.IsTrue(registry.Has(0));
    }
}